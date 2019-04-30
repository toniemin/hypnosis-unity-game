using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script that detects when the player is seen by the CCTV
 * First draft
 */

public class PlayerSeenByCCTV : MonoBehaviour
{
    //public Rigidbody visionRigidbody;

    public GameObject visionCCTV; //GameObject of CCTV's vision
    public GameObject player; //GameObject of the player's character
    public Rigidbody playerRigidbody;

    void Start()
    {
        visionCCTV.GetComponent<Renderer>().enabled = false; //makes the vision cylinder invisible
    }

    void Update()
    {
        //Debug.Log("Console");
    }

    //Called when visionCCTV collides with a GameObject
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Collision");

        /*
          if (col.gameObject == playerRigidbody)
        {
            Debug.Log("You have been spotted!");
            //Destroy(col.gameObject);
        }
        */
    }
}
