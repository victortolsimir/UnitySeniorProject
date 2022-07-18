using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGameMessage : MonoBehaviour
{
    [SerializeField]
    private Text message;

    public void SetText(string desiredText)
    {
        message.text = desiredText;
    }
}
