using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageEntity : MonoBehaviour
{
    protected Queue<string> _messageQueue = null;

    /// <summary>
    /// Translate message put message to messageQueue
    /// </summary>
    /// <param name="message"></param>
    public virtual void TranslateMessage(string message)
    {
        if (_messageQueue != null)
        {
            _messageQueue.Enqueue(message);
        }
    }

    /// <summary>
    /// Each Message Entity defines what message and how it reacts to
    /// </summary>
    protected virtual void HandleMessages()
    {

    }

	// Use this for initialization
	protected virtual void Start ()
    {
        _messageQueue = new Queue<string>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleMessages();
	}
}
