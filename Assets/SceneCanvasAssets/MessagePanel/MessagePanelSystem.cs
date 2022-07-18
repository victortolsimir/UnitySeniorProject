using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePanelSystem : MonoBehaviour
{

    public static MessagePanelSystem instance;

    [SerializeField]
    private CustomGameMessage gameMessage;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HideMessage();
        }
    }

    public static void ShowMessage(string message)
    {
        instance.gameMessage.SetText(message);
        instance.transform.GetChild(0).gameObject.SetActive(true);
    }

    public static void HideMessage()
    {
        instance.transform.GetChild(0).gameObject.SetActive(false);
    }


}
