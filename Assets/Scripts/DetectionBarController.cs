using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DetectionBarController : MonoBehaviour
{
    // Reference to the sliders and status text.
    public Slider sliderLeft;
    public Slider sliderRight;
    public Text statusLabel;

    // Variables to control the slider behaviour.
    public float decreaseDelay = 3f;
    public float increaseRate = .1f;
    public float decreaseRate = .1f;

    bool hasBeenCalled = false;

    // Current slider value.
    public float currentValue { get; private set; }

    // Status text strings/states.
    private const string TEXT_UNSEEN = "Hidden";
    private const string TEXT_DETECTED = "Detected";
    
    // Returns whether the sliders have reached their max value.
    public bool reachedMax()
    {
        return Mathf.Approximately(sliderLeft.value, 1.0f);
    }
    
    // Start increasing the slider value with default rate until 1 or stopped.
    public void StartIncreasing(System.Action callback)
    {
        StartIncreasing(callback, increaseRate);
    }

    // Start increasing the slider value with custom rate until 1 or stopped.
    public void StartIncreasing(System.Action callback, float rate)
    {
        //hasBeenCalled = false;
        StartCoroutine(Increase(callback, rate));
        // Set status text.
        statusLabel.text = TEXT_DETECTED;
    }

    // Stop increasing and after a delay, start decreasing the slider value until 0.
    public void StopIncreasing(System.Action callback)
    {
        //hasBeenCalled = true;
        // Set status text.
        statusLabel.text = TEXT_UNSEEN;

        StopAllCoroutines();
        StartCoroutine(Decrease(callback, decreaseDelay, decreaseRate));

        hasBeenCalled = true;
    }

    private void Start()
    {
        currentValue = 0.0f;

        // Set status text.
        statusLabel.text = TEXT_UNSEEN;

        UpdateSliders();
    }

    private void UpdateSliders()
    {
        sliderLeft.value = currentValue;
        sliderRight.value = currentValue;
    }

    public void ResetSliders()
    {
        sliderLeft.value = 0.0f; 
        sliderRight.value = 0.0f; 
    }

    public bool HasBeenCalled()
    {
        return hasBeenCalled;
    }

    public void setHasBeenCalledValue(bool value)
    {
        hasBeenCalled = value;
    }

    // Wait for the delay and start decreasing slider values until 0.
    private IEnumerator Decrease(System.Action callback, float delay, float rate)
    {
        yield return new WaitForSeconds(delay);

        while (! Mathf.Approximately(currentValue, 0))
        {
            currentValue = Mathf.MoveTowards(currentValue, 0, decreaseRate * Time.deltaTime);

            UpdateSliders();

            yield return null;
        }

        hasBeenCalled = false;
        Debug.Log("Decrease finished");
        callback();
        //hasBeenCalled = false;

    }

    // Start increasing slider value until 1 or stopped.
    private IEnumerator Increase(System.Action callback, float rate)
    {
        while (! Mathf.Approximately(currentValue, 1))
        {
            currentValue = Mathf.MoveTowards(currentValue, 1, increaseRate * Time.deltaTime);

            UpdateSliders();

            yield return null;
        }

        callback();
    }
}
