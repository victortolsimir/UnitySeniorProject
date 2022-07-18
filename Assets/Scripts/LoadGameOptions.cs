using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadGameOptions : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject charactersPanel;
    [SerializeField]
    private GameObject templateButton;
    [SerializeField]
    private GameObject WarningPanel;
    private RectTransform content;
    private string nameToLoad;
    private GameObject currentSelected;

    private void Start()
    {
        content = charactersPanel.GetComponent<ScrollRect>().content;
        ListOptions();
    }

    private void ListOptions()
    {
        var characters = new List<string>();
        var savePath = GlobalControl.instance.SavePath;

        if (Directory.Exists(savePath))
        {
            var filePaths = Directory.GetFiles(savePath);
            foreach (var filePath in filePaths)
            {
                if (Path.GetExtension(filePath).Equals(".txt"))
                    characters.Add(Path.GetFileNameWithoutExtension(filePath));
            }
        }
        characters.ForEach(character => AddButtons(character));
    }

    public void DeleteSave()
    {
        string name = currentSelected.transform.Find("Name").GetComponent<Text>().text;
        string savefilePath = $"{GlobalControl.instance.SavePath}{name}.txt";
        string imagefilePath = $"{GlobalControl.instance.SavePath}{name}.png";
        if (File.Exists(savefilePath))
        {
            File.Delete(savefilePath);
            File.Delete(imagefilePath);
        }
        ClearSaves();
        ListOptions();
    }

    private void ClearSaves()
    {
        for(int i = 1; i < content.childCount; i++)
        {
            GameObject.Destroy(content.GetChild(i).gameObject);
        }
    }

    public void OnBackButton()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnLoadGameButton()
    {
        Debug.Log("Load Game");
        GlobalControl.instance.Load(nameToLoad);
    }

    public void ShowWarning()
    {
        if(currentSelected != null)
            WarningPanel.SetActive(true);

    }

    public void Warning_Yes()
    {
        DeleteSave();
        HideWarning();
    }

    public void HideWarning()
    {
        WarningPanel.SetActive(false);
    }

    private void AddButtons(string name)
    {
        GameObject newButton = Instantiate(templateButton, content);
        Text nameField = newButton.transform.Find("Name").GetComponent<Text>();
        newButton.name = name;
        nameField.text = name;

        //Assign save image based on name
        var savePath = GlobalControl.instance.SavePath;
        Image imageField = newButton.transform.Find("Mask").Find("SaveImage").GetComponent<Image>();
        Texture2D texture = LoadPNG($"{savePath}{name}.png");
        if (texture != null)
        {
            Sprite pathImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            imageField.sprite = pathImage;
        }
            
        newButton.GetComponent<Button>().onClick.AddListener(delegate { SetName(name); });
        newButton.SetActive(true);
    }
    private void SetName(string name)
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != null)
            currentSelected.transform.Find("ToggleBorder").gameObject.SetActive(false);
        currentSelected = button;
        currentSelected.transform.Find("ToggleBorder").gameObject.SetActive(true);
        nameToLoad = name;
    }

    private static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
