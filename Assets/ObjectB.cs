using UnityEngine;
using System.Collections;

public class ObjectB : MessageEntity
{
    private float timer = 10f;
    private bool isTriggered = false;

    protected override void Start()
    {
        base.Start();
        // Listens for "Foo" event
        EventSystem.instance.RegisterListener("Foo", this);
    }

    /// <summary>
    /// ObjectB's implementation of HandleMessages
    /// </summary>
    protected override void HandleMessages()
    {
        while (_messageQueue.Count > 0)
        {
            string message = _messageQueue.Dequeue();
            // Reacts to "Foo" event
            switch (message)
            {
                case "Foo":
                    Debug.Log("Foo Activated!");
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
        // In 10 seconds, this object is going to trigger Foo event
        if (timer < 0 && !isTriggered)
        {
            isTriggered = true;
            SendBarEvent();
        }
    }

    void OnDestroy()
    {
        EventSystem.instance.UnregisterListener("Foo", this);
    }

    public void SendBarEvent()
    {
        // Raises Bar event
        EventSystem.instance.DistributeMessage("Bar");
    }
}