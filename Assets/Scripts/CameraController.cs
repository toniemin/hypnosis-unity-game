using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform; // Reference to player's transform for positioning.

    public float height = 20f; // Height in relation to the player (y-axis).

    public float distance = -20f; // How far away from the player the camera is (z-axis).

    void Awake()
    {
        // Get player's position.
        Vector3 playerPos = playerTransform.position;

        // Set camera position. Control distance using 'height' (for y-axis) and 'distance' (for z-axis) variables.
        // The position on the x-asis is the same as the player.
        transform.position = new Vector3(playerPos.x, playerPos.y + height, playerPos.z + distance);

        // Turn the camera towards the player.
        transform.LookAt(playerPos);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        { 
            // Update player's position. 
            Vector3 playerPos = playerTransform.position; 
            
            // Update camera position. Control distance using 'height' (for y-axis) and 'distance' (for z-axis) variables. 
            // The position on the x-asis is the same as the player. 
            transform.position = new Vector3(playerPos.x, playerPos.y + height, playerPos.z + distance); 
             
            // Turn the camera towards the player. 
            transform.LookAt(playerPos); 
        } 
    }
}
