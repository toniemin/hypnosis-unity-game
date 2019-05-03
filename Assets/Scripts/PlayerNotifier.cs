using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PlayerNotifier class for handling Observer patterns attached to Player
 */
public class PlayerNotifier : Observer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNotify(ObserverEvent ev)
    {
        if (ev.eventName == "collision")
        {
            Debug.Log("You have been spotted! Sincerely, a PlayerNotifier class object");
            //Application.Quit(); //quits the game
        }
            //Debug.Log("PlayerNotifier has been notified of a collusion");
    }

}
