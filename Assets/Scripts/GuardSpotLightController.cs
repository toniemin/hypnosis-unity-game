using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpotLightController : GuardController
{
    public GameObject guard;
    public GameObject collisionCylinder;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //collisionCylinder.GetComponent<Renderer>().enabled = false;
        //collisionCylinder.transform.position = spotlight.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        collisionCylinder.transform.position = guard.transform.position; //Step 1

        //float z = collisionCylinder.transform.position.z * 1.25f; //Step 2
        //float x = collisionCylinder.transform.position.x * 0.55f; //Step 2
        //float y = collisionCylinder.transform.position.y + 0.55f; //Step 2
        //Vector3 newPos = new Vector3(collisionCylinder.transform.position.x, collisionCylinder.transform.position.y, z);
        //collisionCylinder.transform.position = newPos;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision");

        if (col.gameObject.tag.Equals("Wall"))
        {
            guard.GetComponent<GuardController>().wallInSight = true;
            
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag.Equals("Wall"))
            guard.GetComponent<GuardController>().wallInSight = false;
    }

}
