using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{ 

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void OpenGameInfoPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
