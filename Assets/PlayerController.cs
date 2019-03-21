using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody playerRigidbody;

    public const float MAXSPEED = 10f;

    public GameObject visionCCTV; //GameObject of CCTV's vision


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("");
    }

    // Update is called once per frame
    void Update()
    {
        // Apply force according to which axis are being pressed (WASD). Clamp the speed to MAXSPEED.
        playerRigidbody.AddForce(new Vector3(
            Mathf.Clamp(Input.GetAxis("Horizontal")*MAXSPEED, -MAXSPEED, MAXSPEED), 
            0, 
            Mathf.Clamp(Input.GetAxis("Vertical")*MAXSPEED, -MAXSPEED, MAXSPEED)));

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
            Destroy(playerRigidbody.gameObject); //destroys the player character
            Application.Quit(); //quits the game

        }
    }
}
