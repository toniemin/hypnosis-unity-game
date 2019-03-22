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
        // Read movement direction from the keyboard using Input.GetAxis
        // and apply speed to it.
        Vector3 direction = new Vector3(
            Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime,
            0,
            Input.GetAxis("Vertical")* movementSpeed * Time.deltaTime
        );

        // Move player to new positon.
        rb.MovePosition(transform.position + direction);

        // Rotate the player to the movement direction.
        //Quaternion rotation = Quaternion.Euler(0f, rb.rotation.y + Input.GetAxis("Horizontal")*1000f*Time.deltaTime, 0f);
        //rb.MoveRotation(rotation);
        //Quaternion rotation = Input.GetAxisRaw("Horizontal");
        rb.MoveRotation(Quaternion.Euler(0f, 1f, 0f));

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
