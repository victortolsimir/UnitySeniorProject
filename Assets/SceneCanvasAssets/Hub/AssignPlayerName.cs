using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignPlayerName : MonoBehaviour
{
    private void Awake()
    {
        string name = GlobalControl.instance.playerName;
        if(!name.Equals(""))
            transform.GetComponent<Text>().text = name;
    }
}
