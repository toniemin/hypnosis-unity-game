using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float walkSpeed = 20f; // Player's normal movement speed.
    public float sneakSpeed = 10f; // Player's sneaking movement speed.
    public float runSpeed = 35f; // Player's running speed.
    public float rotationSpeed = 10f; // Player's rotation speed.

    private Vector3 movement;

    public GameObject visionCCTV; //GameObject of CCTV's vision
    //public GameObject cameraItself; //GameObject of the CCTV camera itself

    Subject playerSubject = new Subject();
    Subject soundSubject = new Subject();

    void Awake()
    {
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

        // Read movement direction from the keyboard using Input.GetAxis
        // and apply speed to it.
        float speed = (moveH != 0 && moveV != 0) ? movementSpeed * 0.75f : movementSpeed;
        movement = new Vector3(
            moveH * speed,
            0,
            moveV * speed
        ) * Time.deltaTime;
        
        if (Input.GetKey("escape"))
        {
            Application.Quit(); //quits the game if the player presses ESC
        }
    }

    void FixedUpdate()
    {
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

            ObserverEvent collision = new ObserverEvent("collision");
            playerSubject.Notify(collision); //Observer pattern to notify of collision

            Destroy(gameObject); //destroys the player character

            //Application.Quit(); //quits the game

        }
    }
}


