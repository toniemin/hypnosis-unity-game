using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Observer pattern implementation
 * Observer, ObserverEvent and Subject classes
 */

public abstract class Observer
{
    public abstract void OnNotify(ObserverEvent ev);
}

public class Subject
{
    //A list with observers that are waiting for something to happen
     List<Observer> observers = new List<Observer>();

    //Send notifications if something has happened
    public void Notify(ObserverEvent ev)
    {
        for (int i = 0; i < observers.Count; i++)
         {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnNotify(ev);
         }
    }

    //Add observer to the list
    public void AddObserver(Observer observer)
    {
       observers.Add(observer);
    }

    //Remove observer from the list
    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }
    
}

public class ObserverEvent
{
    public string eventName { get; private set; }

    public ObserverEvent(string eventName)
    {
        this.eventName = eventName;
    }
}
