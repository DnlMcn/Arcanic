using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Scriptable Objects/Architecture/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    public void Raise()
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener eventListener)
    { 
        if (!eventListeners.Contains(eventListener))
            eventListeners.Add(eventListener); 
    }

    public void UnregisterListener(GameEventListener eventListener)
    { 
        if (eventListeners.Contains(eventListener))
            eventListeners.Remove(eventListener);  
    }
}
