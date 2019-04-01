using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player; // Reference to the player.

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Vector pointing towards player.
        Vector3 playerDir = player.transform.position;

        float angle = Vector3.SignedAngle(Vector3.forward, playerDir, Vector3.zero);
         
        Vector3 direction = (angle > 0f) ? Vector3.right : Vector3.left;

        //transform.Translate(direction);
    }
}
