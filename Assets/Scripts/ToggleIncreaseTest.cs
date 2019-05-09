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

    public void Toggle(bool state)
    {
        if (state)
        {
            script.StartIncreasing();
        }
        else
        {
            script.StopIncreasing();
        }
    }

    IEnumerator Test()
    {
        Debug.Log("CurrentValue: " + script.currentValue);
        Debug.Log("Starting increase!");
        script.StartIncreasing();


        yield return new WaitForSeconds(3f);
        Debug.Log("CurrentValue: " + script.currentValue);
        yield return new WaitForSeconds(1f);

        Debug.Log("Stopping increase!");
        script.StopIncreasing();
        Debug.Log("CurrentValue " + script.currentValue);
    }
}
