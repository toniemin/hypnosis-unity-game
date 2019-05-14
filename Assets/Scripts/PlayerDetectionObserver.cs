using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetectionObserver : Notifier, IObserver
{

    public Slider detectionSlider1;
    public Slider detectionSlider2;
    public Text detectionText;

    public Notifier[] guards;
    
    public GameObject endScreen;

    System.Action onDetectionMax;

    public float increaseRate = .1f;
    public float decreaseRate = .1f;

    public float DetectionValue { get; private set; } = 1;

    Dictionary<string, string> detectors = new Dictionary<string, string>();

    bool isDetected = false;

    void Awake()
    {
        IObserver observer = endScreen.GetComponent<IObserver>();
        AddObserver(observer);
    }

    private void Start()
    {
        
        foreach (Notifier guard in guards)
        {
            guard.AddObserver(this);
        }

        StartCoroutine(DepleteDetection());
        StartCoroutine(RefillDetection());
        StartCoroutine(UpdateUI());
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

    IEnumerator UpdateUI()
    {
        while (true)
        {
            detectionSlider1.value = DetectionValue;
            detectionSlider2.value = DetectionValue;
            detectionText.text = (isDetected ? "Detected" : "Unseen");

            yield return null;
        }
    }
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
                    Notify(new ObserverEvent("gameover"));
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
            else
            {
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }
}
