using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSystem : MonoBehaviour {

    public static EventSystem instance = null;

    // A dictionary that maps in between message and listeners, notice there could be more than listener for each message
    private Dictionary<string, List<MessageEntity>> listeners = new Dictionary<string, List<MessageEntity>>();

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Send message to all listeners that registered
    /// the event
    /// </summary>
    /// <param name="message"></param>
    private void DispatchToListeners(string message)
    {
        if (listeners.ContainsKey(message))
        {
            List<MessageEntity> msgListners = listeners[message];
            for (int i = 0; i < msgListners.Count; i++)
            {
                msgListners[i].TranslateMessage(message);
            }
        }
        else
        {
            Debug.LogWarning("No such event name " + message);
        }
    }

    /// <summary>
    /// Register a message to a given listner(MessageEntity)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="listener"></param>
    public void RegisterListener(string message, MessageEntity listener)
    {
        if (listeners.ContainsKey(message))
        {
            List<MessageEntity> msgListners = listeners[message];
            if (!msgListners.Contains(listener))
            {
                msgListners.Add(listener);
            }
        }
        else
        {
            List<MessageEntity> msgListener = new List<MessageEntity>();
            msgListener.Add(listener);
            listeners.Add(message, msgListener);
        }
    }

    /// <summary>
    /// Unregister a message from a given listener (MessageEntity)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="listener"></param>
    public void UnregisterListener(string message, MessageEntity listener)
    {
        if (listeners.ContainsKey(message))
        {
            List<MessageEntity> msgListeners = listeners[message];
            msgListeners.Remove(listener);
        }
        else
        {
            Debug.LogWarning("Trying to remove non-existant Listener: " + listener.name + " message: " + message);
        }
    }

    /// <summary>
    /// Public api for different objects to communute, similar to Unity's "Sendmessage"
    /// </summary>
    /// <param name="message"></param>
    public void DistributeMessage(string message)
    {
        DispatchToListeners(message);
    }
}
