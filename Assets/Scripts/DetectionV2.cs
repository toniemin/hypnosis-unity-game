using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionV2: MonoBehaviour, IObserver
{

    public GameObject detectionUI;

    public System.Action onDetectionMax;

    public float increaseRate = .1f;
    public float decreaseRate = .1f;

    public float DetectionValue { get; private set; } = 1;

    Dictionary<string, string> detectors = new Dictionary<string, string>();

    bool isDetected = false;

    private void Start()
    {
        StartCoroutine(DepleteDetection());
        StartCoroutine(RefillDetection());
    }

    public void OnNotify(ObserverEvent env)
    {
        string[] parts = env.eventName.Split(':');
        string detector = parts[0];
        string status = parts[1];

        detectors[detector] = status;

        isDetected = detectors.ContainsValue("detected");
    }

    //void playerDetected()
    //{
    //    gameOverPanel.SetActive(true);
    //    hideTimebar();
    //    Destroy(player.gameObject);
    //}

    IEnumerator DepleteDetection()
    {
        while (true)
        {
            if (isDetected)
            {
                if (! Mathf.Approximately(DetectionValue, 0))
                {
                    DetectionValue = Mathf.MoveTowards(DetectionValue, 0, decreaseRate * Time.deltaTime);
                }
                else
                {
                    onDetectionMax();
                }
            }


            yield return null;
        }
    }

    IEnumerator RefillDetection()
    {
        while (true)
        {
            if (!isDetected)
            {
                if (! Mathf.Approximately(DetectionValue, 1))
                {
                    DetectionValue = Mathf.MoveTowards(DetectionValue, 1, increaseRate * Time.deltaTime);
                }
            }

            yield return null;
        }
    }
}
