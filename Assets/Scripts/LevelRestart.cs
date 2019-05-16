using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    public int currentSceneIndex;

    public void reloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
    }
}
