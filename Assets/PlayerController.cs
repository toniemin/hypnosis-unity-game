using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody playerRigidbody;

    public const float MAXSPEED = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Apply force according to which axis are being pressed (WASD). Clamp the speed to MAXSPEED.
        playerRigidbody.AddForce(new Vector3(
            Mathf.Clamp(Input.GetAxis("Horizontal")*MAXSPEED, -MAXSPEED, MAXSPEED), 
            0, 
            Mathf.Clamp(Input.GetAxis("Vertical")*MAXSPEED, -MAXSPEED, MAXSPEED)));
    }
}
