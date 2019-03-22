using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidbody;

    public float maxSpeed = 10f;

    public GameObject visionCCTV; //GameObject of CCTV's vision

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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
            Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime,
            0,
            Input.GetAxis("Vertical")* maxSpeed * Time.deltaTime
        );

        // Move player to new positon.
        playerRigidbody.MovePosition(transform.position + direction);

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
