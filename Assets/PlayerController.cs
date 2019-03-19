using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            playerRigidbody.AddForce(new Vector3(0, 0, 100f));
        }
        if (Input.GetKeyDown("d"))
        {
            playerRigidbody.AddForce(new Vector3(100f, 0, 0));
        }
        if (Input.GetKeyDown("s"))
        {
            playerRigidbody.AddForce(new Vector3(0, 0, -100f));
        }
        if (Input.GetKeyDown("a"))
        {
            playerRigidbody.AddForce(new Vector3(-100f, 0, 100f));
        }
    }
}
