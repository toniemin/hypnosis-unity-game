using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player; // Reference to the player.

    private float distance;

    void Awake()
    {
        // Calculate distance between player and camera.
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
