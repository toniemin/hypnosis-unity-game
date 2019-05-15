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

        endScreen.SetActive(false);
        detectionSlider1.gameObject.SetActive(false);
        detectionSlider2.gameObject.SetActive(false);
        detectionText.gameObject.SetActive(false);
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
        //endScreen.SetActive(true);

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
                detectionSlider1.gameObject.SetActive(true);
                detectionSlider2.gameObject.SetActive(true);
                detectionText.gameObject.SetActive(true);

                if (! Mathf.Approximately(DetectionValue, 0))
                {
                    DetectionValue = Mathf.MoveTowards(DetectionValue, 0, decreaseRate * Time.deltaTime);
                }
                else
                {
                    //endScreen.SetActive(true);
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

                else
                {
                    detectionSlider1.gameObject.SetActive(false);
                    detectionSlider2.gameObject.SetActive(false);
                    detectionText.gameObject.SetActive(false);
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
