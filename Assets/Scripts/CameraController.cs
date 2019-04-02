using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player.

    public float height = 20f; // Height in relation to the player (y-axis).

    public float distance = 20f; // How far away from the player the camera is (z-axis).

    private Vector3 position;

    void Awake()
    {
        // Get player's position.
        Vector3 playerPos = player.transform.position;

        // Set height in relation to player's position.
        height -= playerPos.y;
        // Set distance in relation to player's position.
        distance += playerPos.z;
        // Player's position on the x-asis.
        float playerX = playerPos.x;

        // Apply position.
        position = new Vector3(playerX, height, distance);
        transform.Translate(position);

        // Turn camera towards the player.
        transform.LookAt(playerPos);
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        if (position.x != playerX)
        {
            position.x = playerX;
            transform.Translate(position);
            transform.LookAt(player.transform.position);
        }
    }
}
