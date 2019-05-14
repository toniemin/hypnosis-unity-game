using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionObserver : MonoBehaviour, IObserver
{

    public GameObject detectionUI;

    public Notifier[] guards;

    public System.Action onDetectionMax;

    public float increaseRate = .1f;
    public float decreaseRate = .1f;

    public float DetectionValue { get; private set; } = 1;

    Dictionary<string, string> detectors = new Dictionary<string, string>();

    bool isDetected = false;

    private void Start()
    {
        IObserver observer = GetComponent<IObserver>();
        foreach (Notifier guard in guards)
        {
            guard.AddObserver(observer);
        }

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
                    Debug.Log("Game ended!");
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
