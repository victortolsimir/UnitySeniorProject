using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject confirmationPanel;

    public void OnMainMenuButton()
    {
        confirmationPanel.SetActive(!confirmationPanel.activeSelf);
    }

    public void OnConfirmButton()
    {
        SceneLoader.Load("MainMenu");
    }

    public void OnCancelButton()
    {
        confirmationPanel.SetActive(false);
    }
}
