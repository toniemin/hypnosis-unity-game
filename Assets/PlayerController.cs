using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;

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
        float moveH = Input.GetAxis("Horizontal"); // Horizontal movement.
        float moveV = Input.GetAxis("Vertical"); // Vertical movement.

        // Read movement direction from the keyboard using Input.GetAxis
        // and apply speed to it.
        float speed = (moveH != 0 && moveV != 0) ? movementSpeed * 0.75f : movementSpeed;
        Vector3 movement = new Vector3(
            moveH * speed * Time.deltaTime,
            0,
            moveV * speed * Time.deltaTime
        );
        // Move player to new positon.
        rb.MovePosition(transform.position + movement);

        // Turn player to face the moving direction.
        if (movement != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            //rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 0.15F));
            rb.MoveRotation(rotation);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit(); //quits the game if the player presses ESC
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


