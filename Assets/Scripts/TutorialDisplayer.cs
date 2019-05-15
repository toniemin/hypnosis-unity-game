/**
 * Hypnosis-unity-game (Github: https://github.com/toniemin/hypnosis-unity-game )
 * 
 * TutorialDisplay is a script for the Tutorial-prefab that when placed into a level, 
 * displays tutorial images to the player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDisplayer : MonoBehaviour
{

    // UI-element for image output.
    public RawImage output;

    // Reference to the main camera in the level and the camera that shows the images.
    public Camera sceneMainCamera;
    public Camera tutorialCamera;
    
    // Arrray to hold showable images.
    public Texture2D[] images;
    // Current index of images-array.
    int imagesIndex = 0;

    // Tutorial camera disabled by default.
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

        // Change player's camera to the tutorial camera.
        sceneMainCamera.enabled = false;
        tutorialCamera.enabled = true;

        NextImage();
    }

    private void Update()
    {
        // Show next image when the player press "Jump" (default: Spacebar).
        if( Input.GetButtonDown("Jump") )
        {
            NextImage();
        }
    }

    // Show the next image from the image-array and
    // destroy the gameobject if no images remain.
    void NextImage()
    {
        output.texture = images[imagesIndex];

        imagesIndex++;

        if (imagesIndex == images.Length)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Change player's camera back to the level's main camera
        // upon gameObject's destruction.
        if (tutorialCamera != null)
        {
            tutorialCamera.enabled = false;
        }

        if (tutorialCamera != null)
        {
            sceneMainCamera.enabled = true;
        }
    }
}
