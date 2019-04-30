using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 rot = new Vector3(h, 0, v);
            rb.MoveRotation(Quaternion.LookRotation(rot));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
