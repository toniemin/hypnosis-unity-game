using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float walkSpeed = 10f; // Player's normal movement speed.
    private float sneakSpeed = 5f; // Player's sneaking movement speed.
    private float runSpeed = 17f; // Player's running speed.
    private float rotationSpeed = 5f; // Player's rotation speed.

    private Vector3 movement;

    public GameObject visionCCTV; //GameObject of CCTV's vision
    //public GameObject cameraItself; //GameObject of the CCTV camera itself

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("***********");
        //// Set default movement speed. Change if needed.
        //float movementSpeed = walkSpeed;

        //// Check if player is sneaking.
        //if (Input.GetButton("Sneak"))
        //{
        //    movementSpeed = sneakSpeed;
        //}
        //// Check if player is running.
        //if (Input.GetButton("Sprint"))
        //{
        //    movementSpeed = runSpeed;
        //}

//////////////////////////////////////

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



        float moveH = Input.GetAxis("Horizontal"); // Horizontal movement.
        float moveV = Input.GetAxis("Vertical"); // Vertical movement.

        if (Input.GetButtonDown("Horizontal"))
            Debug.Log("moveH: " + moveH.ToString());
        //Debug.Log("moveV: " + moveV.ToString());

        // Read movement direction from the keyboard using Input.GetAxis
        // and apply speed to it.
        float speed = (moveH != 0 && moveV != 0) ? movementSpeed * 0.75f : movementSpeed;
        movement = new Vector3(
            moveH * speed,
            0,
            moveV * speed
        ) * Time.fixedDeltaTime;

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
        //Debug.Log("Collision");

        if (col.gameObject == visionCCTV)
        {
            //Debug.Log("You have been spotted!");

            gameOverPanel.SetActive(true);

            ObserverEvent collision = new ObserverEvent("collision");
            playerSubject.Notify(collision); //Observer pattern to notify of collision

            Destroy(gameObject); //destroys the player character

            

            //Application.Quit(); //quits the game

        }
    }
}


