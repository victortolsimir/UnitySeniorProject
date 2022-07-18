using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject globalControlPrefab;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject loadGamePanel;
    [SerializeField]
    private GameObject questControllerPrefab;

    public void OnNewGameButton()
    {
        if (!GlobalControl.instance)
            Instantiate(globalControlPrefab);
        if (!QuestController.instance)
            Instantiate(questControllerPrefab);
        SceneManager.LoadScene("CharacterCreation");
    }

    public void OnLoadGameButton()
    {
        if (!GlobalControl.instance)
            Instantiate(globalControlPrefab);
        if (!QuestController.instance)
            Instantiate(questControllerPrefab);

        // GlobalControl.instance.Load();

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        button.transform.Find("arrow").gameObject.SetActive(false);
        
        loadGamePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnSettingsButton()
    {
        //grab the button's gameobject from event system
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        button.transform.Find("arrow").gameObject.SetActive(false);

        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
