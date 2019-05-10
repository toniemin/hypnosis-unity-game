using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIncreaseTest : MonoBehaviour
{
    public GameObject controller;

    private DetectionBarController script;

    void Start()
    {
        script = controller.GetComponent<DetectionBarController>();
    }

    void log()
    {
        Debug.Log("callback, time's up!");
    }

    public void press()
    {
        Toggle(true);
    }

    public void Toggle(bool state)
    {
        if (state)
        {
            script.StartIncreasing(log);
        }
        else
        {
            //script.StopIncreasing();
            script.StopIncreasing(log);
        }
    }
}
