using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplayer : MonoBehaviour
{
    public Texture2D[] images;

    public RawImage output;

    public Camera sceneMainCamera;
    public Camera tutorialCamera;

    int index = 0;

    private void Awake()
    {
        tutorialCamera.enabled = false;
    }

    private void Start()
    {
        if (images.Length == 0)
        {
            Debug.Log("Error: Tutorial images missing. Please add tutorial images to TutorialDisplayer!");
            Destroy(gameObject);
        }

        sceneMainCamera.enabled = false;
        tutorialCamera.enabled = true;

        NextImage();
    }

    private void Update()
    {
        if( Input.GetButtonDown("Jump") )
        {
            NextImage();
        }
    }

    void NextImage()
    {
        output.texture = images[index];

        index++;

        if (index == images.Length)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        tutorialCamera.enabled = false;
        sceneMainCamera.enabled = true;
    }
}
