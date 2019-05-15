using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour, IObserver
{
    public Camera mainCamera;
    public Camera endCamera;

    void Awake()
    {
        endCamera.enabled = false;
    }

    public void OnNotify(ObserverEvent env)
    {
        GameOver();
    }

    void GameOver()
    {
        mainCamera.enabled = false;
        endCamera.enabled = true;
        this.gameObject.SetActive(true); //Displays the endScreen
    }
}
