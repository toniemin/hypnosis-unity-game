using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public GameObject GameWinScreen;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameWinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            GameWinScreen.SetActive(true);
        }
    }
}
