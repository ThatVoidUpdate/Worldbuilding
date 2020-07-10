using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageForwarder : MonoBehaviour
{
    public StringEvent MessageRecieved;
    public bool Verbose;
    public void ForwardMessage(string Message)
    {
        MessageRecieved.Invoke(Message);
        if (Verbose)
        {
            Debug.Log(Message);
        }
    }
}
