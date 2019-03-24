using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;

    public GameObject visionCCTV; //GameObject of CCTV's vision

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        Vector3 movement = new Vector3(
            moveH * movementSpeed * Time.deltaTime,
            0,
            moveV * movementSpeed * Time.deltaTime
        );
        // Move player to new positon.
        rb.MovePosition(transform.position + movement);

        // Turn player to face the moving direction.
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F));

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
            Debug.Log("You have been spotted!");
            Destroy(gameObject); //destroys the player character
            Application.Quit(); //quits the game

        }
    }
}
