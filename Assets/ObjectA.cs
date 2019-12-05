using UnityEngine;
using System.Collections;

public class ObjectA : MessageEntity
{
    private float timer = 5f;
    private bool isTriggered = false;

    protected override void Start()
    {
        base.Start();
        // Listens for "Bar" event
        EventSystem.instance.RegisterListener("Bar", this);
    }

    /// <summary>
    /// ObjectA's implementation of HandleMessages
    /// </summary>
    protected override void HandleMessages()
    {
        while (_messageQueue.Count > 0)
        {
            string message = _messageQueue.Dequeue();
            // Reacts to "Bar" event
            switch (message)
            {
                case "Bar":
                    Debug.Log("Bar Activated!");
                    break;
                default:
                    break;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        // In 5 seconds, this object is going to trigger Foo event
        if (timer < 0 && !isTriggered)
        {
            isTriggered = true;
            SendFooEvent();
        }
    }

    void OnDestroy()
    {
        EventSystem.instance.UnregisterListener("Bar", this);
    }

    public void SendFooEvent()
    {
        // Raises Foo event
        EventSystem.instance.DistributeMessage("Foo");
    }
}