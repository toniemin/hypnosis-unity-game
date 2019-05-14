﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransitionScript : MonoBehaviour
{
    public GameObject player;
    public int nextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        { 
            SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
        }
    }
}