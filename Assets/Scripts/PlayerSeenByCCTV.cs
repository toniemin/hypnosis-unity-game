using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script that detects when the player is seen by the CCTV
 * First draft
 */

public class PlayerSeenByCCTV : Notifier
{
    //public Rigidbody visionRigidbody;

    public GameObject visionCCTV; //GameObject of CCTV's vision
    public GameObject objectForCCTV; //GameObject of CCTV
    public GameObject player; //GameObject of the player's character
    public Rigidbody playerRigidbody;
    public Light spotlight;

    bool playerInView = false;
    bool notificationSent = false;

    void Start()
    {
        visionCCTV.GetComponent<Renderer>().enabled = false; //makes the vision cylinder invisible
        spotlight.transform.position = objectForCCTV.transform.position;
    }

    void Update()
    {
        //Debug.Log("Console");
    }

    //public void OnNotify(ObserverEvent env)
    //{
    //    Debug.Log("seen");
    //}

    //Called when visionCCTV collides with a GameObject
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision");

        /*
          if (col.gameObject == playerRigidbody)
        {
            Debug.Log("You have been spotted!");
            //Destroy(col.gameObject);
        }
        */

        //Notify(new ObserverEvent("CCTV" + ":detected"));
        playerInView = true;

    }

    private void OnCollisionExit(Collision col)
    {
        //Notify(new ObserverEvent("CCTV" + ":lost"));
        playerInView = false;
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {

            if (playerInView && !notificationSent)
            {
                Notify(new ObserverEvent("CCTV" + ":detected"));
                notificationSent = true;
            }


            if (!playerInView)
            {
                if (notificationSent)
                {
                    Notify(new ObserverEvent("CCTV" + ":lost"));
                    notificationSent = false;
                }
            }

            yield return null;
        }
    }
}
