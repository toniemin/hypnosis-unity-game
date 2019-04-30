using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    public bool enableDash = false; // Enable dashing mechanics.
    
    private float dashSpeed = 50f; // Speed at which the player dashes when double tapping movement keys.
    private float walkSpeed = 10f; // Player's normal movement speed.
    private float sneakSpeed = 5f; // Player's sneaking movement speed.
    private float runSpeed = 17f; // Player's running speed.
    private float rotationSpeed = 5f; // Player's rotation speed.

    public GameObject visionCCTV; //GameObject of CCTV's vision
    public GameObject gameOverPanel; //Panel for displaying the "You have been spotted!" text

    Subject playerSubject = new Subject();
    Subject soundSubject = new Subject();

    void Awake()
    {
        gameOverPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();

        playerSubject.AddObserver(new PlayerNotifier());
        soundSubject.AddObserver(new SoundNotifier());
    }

    // Update is called once per frame
    void Update()
    {
        if (enableDash)
        {
            StartCoroutine(DoubleTap("Horizontal", true));
            StartCoroutine(DoubleTap("Horizontal", false));
            StartCoroutine(DoubleTap("Vertical", true));
            StartCoroutine(DoubleTap("Vertical", false));
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit(); //quits the game if the player presses ESC
        }
    }

    void FixedUpdate()
    {
        // Set default movement speed. Change if needed.
        float movementSpeed = walkSpeed;

        // Check if player is sneaking.
        if (Input.GetButton("Sneak"))
        {
            movementSpeed = sneakSpeed;
        }
        // Check if player is running.
        if (Input.GetButton("Sprint"))
        {
            movementSpeed = runSpeed;
        }
        
        // Read WASD/arrow-key button presses.
        float moveH = Input.GetAxis("Horizontal"); // Horizontal movement.
        float moveV = Input.GetAxis("Vertical"); // Vertical movement.
        
        // Apply player movement to a vector and clamp diagonal movement.
        Vector3 movement = new Vector3( moveH, 0, moveV );
        Vector3.ClampMagnitude(movement, 1f);

        // Apply movement speed and make it fps-independent.
        movement = movement *movementSpeed * Time.fixedDeltaTime;

        // Move player to new positon.
        rb.MovePosition(transform.position + movement);
        
        // Turn player to face the moving direction.
        if (movement != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(rotation);
        }
    }

    //Called when the playe collides with visionCCTV
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == visionCCTV)
        {
            gameOverPanel.SetActive(true);

            ObserverEvent collision = new ObserverEvent("collision");
            playerSubject.Notify(collision); //Observer pattern to notify of collision

            Destroy(gameObject); //destroys the player character
        }
    }

    IEnumerator DoubleTap(string axis, bool positive)
    {
        float direction = positive ? 1 : -1;
        while (true)
        {
            if (Input.GetAxisRaw(axis) == direction)
            {
                yield return new WaitForSeconds(0.1f);

                if (Input.GetAxisRaw(axis) == 0)
                {
                    yield return new WaitForSeconds(0.1f);

                    if (Input.GetAxisRaw(axis) == direction)
                    {
                        float speed = dashSpeed * direction * Time.deltaTime;

                        Vector3 movement = axis == "Horizontal" ? new Vector3(speed, 0f, 0f) : new Vector3(0f, 0f, speed);

                        rb.MovePosition(transform.position + movement);
                    }
                }
                else
                {
                    yield return null;
                }
            }
            else
                yield return null;
        }
    }
}


