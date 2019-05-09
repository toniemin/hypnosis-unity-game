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
    public float increaseRate = .3f;
    public float decreaseRate = .1f;

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
    public void StartIncreasing()
    {
        StartCoroutine(Increase(increaseRate));

        // Set status text.
        statusLabel.text = TEXT_DETECTED;
    }

    // Start increasing the slider value with custom rate until 1 or stopped.
    public void StartIncreasing(float rate)
    {
        StartCoroutine(Increase(rate));
    }

    // Stop increasing and after a delay, start decreasing the slider value until 0.
    public void StopIncreasing()
    {
        // Set status text.
        statusLabel.text = TEXT_UNSEEN;

        StopAllCoroutines();
        StartCoroutine(Decrease(decreaseDelay, decreaseRate));
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

    // Wait for the delay and start decreasing slider values until 0.
    private IEnumerator Decrease(float delay, float rate)
    {
        yield return new WaitForSeconds(delay);

        while (! Mathf.Approximately(currentValue, 0))
        {
            currentValue = Mathf.MoveTowards(currentValue, 0, decreaseRate * Time.deltaTime);

            UpdateSliders();

            yield return null;
        }
    }

    // Start increasing slider value until 1 or stopped.
    private IEnumerator Increase(float rate)
    {
        while (! Mathf.Approximately(currentValue, 1))
        {
            currentValue = Mathf.MoveTowards(currentValue, 1, increaseRate * Time.deltaTime);

            UpdateSliders();

            yield return null;
        }
    }
}
