using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    //Editor Fields

    [Header("Male Default Item Lists")]
    public List<DefaultItem> hairs;
    public List<DefaultItem> heads;
    public List<DefaultItem> fHairs;
    public List<DefaultItem> bodys;
    public List<DefaultItem> hands;
    public List<DefaultItem> legs;
    public List<DefaultItem> feets;

    [Header("Female Default Item Lists")]
    public List<DefaultItem> fem_hairs;
    public List<DefaultItem> fem_heads;
    public List<DefaultItem> fem_fHairs;
    public List<DefaultItem> fem_bodys;
    public List<DefaultItem> fem_hands;
    public List<DefaultItem> fem_legs;
    public List<DefaultItem> fem_feets;

    [Header("Text fields")]
    public Text hair_text;
    public Text head_text;
    public Text fHair_text;
    public Text body_text;
    public Text hand_text;
    public Text leg_text;
    public Text feet_text;

    [SerializeField]
    private Text nameInputField;
    [SerializeField]
    private Text nameWarning;

    public Text scarColorPanelTitle;

    [Header("Button Selected fields")]
    public GameObject femaleButtonSelected;
    public GameObject maleButtonSelected;
    /*=====================================================================================================================*/

    private bool gender_male = true;

    private MaterialPropertyBlock matPropBlock;

    private GameObject oldHair_colorBlock;
    private GameObject oldSkin_colorBlock;
    private GameObject oldScar_colorBlock;
    private GameObject oldBodyArt_colorBlock;
    private GameObject oldEye_colorBlock;

    private List<List<DefaultItem>> defaultMaleLists;
    private List<List<DefaultItem>> defaultFemaleLists;
    private List<Text> textFields;

    private List<String> textTitles = new List<string>(){ "Hair", "Face", "Facial Hair", "Body", "Hands", "Legs", "Feet" };

    //selected index's
    private readonly int hairIndex = 0;
    private readonly int headIndex = 1;
    private readonly int fHairIndex = 2;
    private readonly int bodyIndex = 3;
    private readonly int handsIndex = 4;
    private readonly int legsIndex = 5;
    private readonly int feetIndex = 6;

    //initially all set to first option
    private int[] maleSelected = { 0, 0, 0, 0, 0, 0, 0 };
    private int[] femaleSelected = { 0, 0, 0, 0, 0, 0, 0 };

    /*=====================================================================================================================*/
    public static CharacterCreation instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        defaultMaleLists = new List<List<DefaultItem>>{ hairs, heads, fHairs, bodys, hands, legs, feets };
        defaultFemaleLists = new List<List<DefaultItem>> { fem_hairs, fem_heads, fem_fHairs, fem_bodys, fem_hands, fem_legs, fem_feets };
        textFields = new List<Text>{ hair_text,head_text,fHair_text,body_text,hand_text,leg_text,feet_text};
        matPropBlock = new MaterialPropertyBlock();

        oldHair_colorBlock = GameObject.Find("AllHairColors/lightbrown/selected");
        oldSkin_colorBlock = GameObject.Find("AllSkinColors/beige/selected");
        oldScar_colorBlock = GameObject.Find("AllScarColors/pink/selected");
        oldBodyArt_colorBlock = GameObject.Find("AllBodyArtColors/purple/selected");
        oldEye_colorBlock = GameObject.Find("AllEyeColors/black/selected");

        EquipAllSelected(defaultMaleLists,maleSelected);
    }

    /*=====================================================================================================================*/

    private void EquipAllSelected(List<List<DefaultItem>> itemLists,int[] selected)
    {
        int i = 0;
        foreach(List<DefaultItem> list in itemLists)
        {
            DefaultItem item = list[selected[i++]];
            if (!item.name.Equals("None"))
                item.Equip();
        }
    }

    private void UnEquipAllSelected(List<List<DefaultItem>> itemLists, int[] selected)
    {
        int i = 0;
        foreach (List<DefaultItem> list in itemLists)
        {
            DefaultItem item = list[selected[i++]];
            if (!item.name.Equals("None"))
                item.UnEquip();
        }
    }
    /*=====================================================================================================================*/

    public void GenderChange()
    {
        //deselect current gender
        if (gender_male)
            maleButtonSelected.SetActive(false);
        else
            femaleButtonSelected.SetActive(false);

        gender_male = !gender_male;
        AdjustGenderView();
        
    }

    private void AdjustGenderView()
    {
        if (gender_male)
        {
            maleButtonSelected.SetActive(true);
            UnEquipAllSelected(defaultFemaleLists, femaleSelected);
            EquipAllSelected(defaultMaleLists,maleSelected);
            SwitchAllFieldText();
            scarColorPanelTitle.text = "Scar Color";
        }
        else
        {
            femaleButtonSelected.SetActive(true);
            UnEquipAllSelected(defaultMaleLists, maleSelected);
            EquipAllSelected(defaultFemaleLists,femaleSelected);
            SwitchAllFieldText();
            scarColorPanelTitle.text = "Scar/Lip Color";
        }
    }

    /*=====================================================================================================================*/

    public void RightArrow(string mode)
    {
        List<List<DefaultItem>> defaultItemList;
        int[] selected;

        if (gender_male)
        {
            defaultItemList = defaultMaleLists;
            selected = maleSelected;
        }
        else
        {
            defaultItemList = defaultFemaleLists;
            selected = femaleSelected;
        }

        int selectedCategory = -1;
        switch (mode)
        {
            case "Hair":
                RemoveOldItem(selected[hairIndex], defaultItemList[hairIndex]);
                selected[hairIndex] = IncreaseSelect(selected[hairIndex],defaultItemList[hairIndex].Count);
                selectedCategory = hairIndex;
                break;
            
            case "Head":
                RemoveOldItem(selected[headIndex], defaultItemList[headIndex]);
                selected[headIndex] = IncreaseSelect(selected[headIndex], defaultItemList[headIndex].Count);
                selectedCategory = headIndex;
                break;
            
            case "Facial Hair":
                RemoveOldItem(selected[fHairIndex], defaultItemList[fHairIndex]);
                selected[fHairIndex] = IncreaseSelect(selected[fHairIndex], defaultItemList[fHairIndex].Count);
                selectedCategory = fHairIndex;
                break;
            
            case "Body":
                RemoveOldItem(selected[bodyIndex], defaultItemList[bodyIndex]);
                selected[bodyIndex] = IncreaseSelect(selected[bodyIndex], defaultItemList[bodyIndex].Count);
                selectedCategory = bodyIndex;
                break;
           
            case "Hands":
                RemoveOldItem(selected[handsIndex], defaultItemList[handsIndex]);
                selected[handsIndex] = IncreaseSelect(selected[handsIndex], defaultItemList[handsIndex].Count);
                selectedCategory = handsIndex;
                break;
            
            case "Legs":
                RemoveOldItem(selected[legsIndex], defaultItemList[legsIndex]);
                selected[legsIndex] = IncreaseSelect(selected[legsIndex], defaultItemList[legsIndex].Count);
                selectedCategory = legsIndex;
                break;
            
            case "Feet":
                RemoveOldItem(selected[feetIndex], defaultItemList[feetIndex]);
                selected[feetIndex] = IncreaseSelect(selected[feetIndex], defaultItemList[feetIndex].Count);
                selectedCategory = feetIndex;
                break;
        }
        UpdateCharacterModel(selectedCategory);
        UpdateFieldText(selectedCategory);
    }

    /*=====================================================================================================================*/
    public void LeftArrow(string mode)
    {
        List<List<DefaultItem>> defaultItemList;
        int[] selected;

        if (gender_male)
        {
            defaultItemList = defaultMaleLists;
            selected = maleSelected;
        }
        else
        {
            defaultItemList = defaultFemaleLists;
            selected = femaleSelected;
        }

        int selectedCategory = -1;
        switch (mode)
        {
            case "Hair":
                RemoveOldItem(selected[hairIndex], defaultItemList[hairIndex]);
                selected[hairIndex] = DecreaseSelect(selected[hairIndex], defaultItemList[hairIndex].Count);
                selectedCategory = hairIndex;
                break;

            case "Head":
                RemoveOldItem(selected[headIndex], defaultItemList[headIndex]);
                selected[headIndex] = DecreaseSelect(selected[headIndex], defaultItemList[headIndex].Count);
                selectedCategory = headIndex;
                break;

            case "Facial Hair":
                RemoveOldItem(selected[fHairIndex], defaultItemList[fHairIndex]);
                selected[fHairIndex] = DecreaseSelect(selected[fHairIndex], defaultItemList[fHairIndex].Count);
                selectedCategory = fHairIndex;
                break;

            case "Body":
                RemoveOldItem(selected[bodyIndex], defaultItemList[bodyIndex]);
                selected[bodyIndex] = DecreaseSelect(selected[bodyIndex], defaultItemList[bodyIndex].Count);
                selectedCategory = bodyIndex;
                break;

            case "Hands":
                RemoveOldItem(selected[handsIndex], defaultItemList[handsIndex]);
                selected[handsIndex] = DecreaseSelect(selected[handsIndex], defaultItemList[handsIndex].Count);
                selectedCategory = handsIndex;
                break;

            case "Legs":
                RemoveOldItem(selected[legsIndex], defaultItemList[legsIndex]);
                selected[legsIndex] = DecreaseSelect(selected[legsIndex], defaultItemList[legsIndex].Count);
                selectedCategory = legsIndex;
                break;

            case "Feet":
                RemoveOldItem(selected[feetIndex], defaultItemList[feetIndex]);
                selected[feetIndex] = DecreaseSelect(selected[feetIndex], defaultItemList[feetIndex].Count);
                selectedCategory = feetIndex;
                break;
        }
        UpdateCharacterModel(selectedCategory);
        UpdateFieldText(selectedCategory);
    }
    /*=====================================================================================================================*/

    private void UpdateFieldText(int category)
    {
        int[] selected;
        if (gender_male) selected = maleSelected;
        else selected = femaleSelected;

        Text field = textFields[category];

        if (category == hairIndex && selected[hairIndex] == 0 || category == fHairIndex && selected[fHairIndex] == 0)
            field.text = "None";
        else
        {
            int id;
            if (category == hairIndex || category == fHairIndex)
                id = selected[category];
            else
                id = selected[category] + 1;
            field.text = textTitles[category] + " " + id;
        }
    }


    private void SwitchAllFieldText()
    {
        int[] selected;
        if (gender_male) selected = maleSelected;
        else selected = femaleSelected;

        for(int category = 0; category < textFields.Count; category++)
        {
            Text field = textFields[category];
            
            //if female grey-out facialhair category
            if(!gender_male && category == fHairIndex)
                field.color = new Color(0.619f, 0.670f, 0.611f, 1.0f);
            else
                field.color = new Color(1.0f, .867f, .627f, 1.0f);

            //Change field text to match selected index 
            if (category == hairIndex && selected[hairIndex] == 0 || category == fHairIndex && selected[fHairIndex] == 0)
                field.text = "None";
            else
            {
                int id;
                if (category == hairIndex || category == fHairIndex)
                    id = selected[category];
                else
                    id = selected[category] + 1;
                field.text = textTitles[category] + " " + id;
            }
        }
    }

    private void RemoveOldItem(int item, List<DefaultItem> itemList)
    {
        DefaultItem oldItem = itemList[item];
        if (!oldItem.name.Equals("None"))
            oldItem.UnEquip();
    }

    

    private int IncreaseSelect(int selectedID, int maxsize)
    {
        if (selectedID == maxsize - 1)
            selectedID = 0;
        else
            selectedID++;
        
        return selectedID;

    }

    private int DecreaseSelect(int selectedID, int maxsize)
    {
        if (selectedID == 0)
            selectedID = maxsize - 1;
        else
            selectedID--;
        
        return selectedID;

    }

    private void UpdateCharacterModel(int category)
    {
        List<List<DefaultItem>> defaultItemList;
        int[] selected;
        
        if (gender_male)
        {
            defaultItemList = defaultMaleLists;
            selected = maleSelected;
        }
        else
        {
            defaultItemList = defaultFemaleLists;
            selected = femaleSelected;
        }

        List<DefaultItem> list = defaultItemList[category];
        DefaultItem item = list[selected[category]];

        if(!item.name.Equals("None"))
            item.Equip();

    }
/*=====================================================================================================================*/
    public void ChangeHairColor()
    {
        //grab the button's gameobject from event system
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Color color = button.transform.Find("colorBox").GetComponent<Image>().color;
        matPropBlock.SetColor("_Color_Hair", color);

        //Assign color to all hair items in list to view on the model
        foreach(DefaultItem hair in hairs)
        {
            if(!hair.name.Equals("None"))
                hair.UpdateItemColorsOnPlayer(matPropBlock);
        }
        foreach (DefaultItem hair in fem_hairs)
        {
            if (!hair.name.Equals("None"))
                hair.UpdateItemColorsOnPlayer(matPropBlock);
        }

        foreach (DefaultItem fhair in fHairs)
        {
            if (!fhair.name.Equals("None"))
                fhair.UpdateItemColorsOnPlayer(matPropBlock);
        }
        foreach (DefaultItem fhair in fem_fHairs)
        {
            if (!fhair.name.Equals("None"))
                fhair.UpdateItemColorsOnPlayer(matPropBlock);
        }

        foreach (DefaultItem face in heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }
        foreach (DefaultItem face in fem_heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }

        //show that color is selected
        SelectHairButton(button);
        oldHair_colorBlock = button.transform.GetChild(0).gameObject;

    }

    private void SelectHairButton(GameObject button)
    {
        oldHair_colorBlock.SetActive(false);
        button.transform.GetChild(0).gameObject.SetActive(true);

    }
    /*=====================================================================================================================*/

    public void ChangeSkinColor()
    {
        //grab the button's gameobject from event system
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Color color = button.transform.Find("colorBox").GetComponent<Image>().color;
        matPropBlock.SetColor("_Color_Skin", color);

        //Adjust stubble color
        Color stubble = new Color(color.r - .123f, color.g - .018f, color.b + .0336f);
        matPropBlock.SetColor("_Color_Stubble", stubble);

        foreach(List<DefaultItem> parts in defaultMaleLists)
        {
            foreach(DefaultItem item in parts)
            {
                if(!item.name.Equals("None"))
                    item.UpdateItemColorsOnPlayer(matPropBlock);
            }
        }
        foreach (List<DefaultItem> parts in defaultFemaleLists)
        {
            foreach (DefaultItem item in parts)
            {
                if (!item.name.Equals("None"))
                    item.UpdateItemColorsOnPlayer(matPropBlock);
            }
        }
        SelectSkinButton(button);
        oldSkin_colorBlock = button.transform.GetChild(0).gameObject;
    }

    private void SelectSkinButton(GameObject button)
    {
        oldSkin_colorBlock.SetActive(false);
        button.transform.GetChild(0).gameObject.SetActive(true);
    }

    /*=====================================================================================================================*/
    public void ChangeScarColor()
    {
        //grab the button's gameobject from event system
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Color color = button.transform.Find("colorBox").GetComponent<Image>().color;
        matPropBlock.SetColor("_Color_Scar", color);

        foreach (DefaultItem face in heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }
        foreach (DefaultItem face in fem_heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }


        SelectScarButton(button);
        oldScar_colorBlock = button.transform.GetChild(0).gameObject;
    }

    private void SelectScarButton(GameObject button)
    {
        oldScar_colorBlock.SetActive(false);
        button.transform.GetChild(0).gameObject.SetActive(true);
    }
    /*=====================================================================================================================*/
    public void ChangeBodyArtColor()
    {
        //grab the button's gameobject from event system
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Color color = button.transform.Find("colorBox").GetComponent<Image>().color;
        matPropBlock.SetColor("_Color_BodyArt", color);

        foreach (DefaultItem face in heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }
        foreach (DefaultItem face in fem_heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }

        SelectBodyArtButton(button);
        oldBodyArt_colorBlock = button.transform.GetChild(0).gameObject;
    }

    private void SelectBodyArtButton(GameObject button)
    {
        oldBodyArt_colorBlock.SetActive(false);
        button.transform.GetChild(0).gameObject.SetActive(true);
    }
    /*=====================================================================================================================*/
    public void ChangeEyeColor()
    {
        //grab the button's gameobject from event system
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Color color = button.transform.Find("colorBox").GetComponent<Image>().color;
        matPropBlock.SetColor("_Color_Eyes", color);

        foreach (DefaultItem face in heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }
        foreach (DefaultItem face in fem_heads)
        {
            face.UpdateItemColorsOnPlayer(matPropBlock);
        }

        SelectEyeButton(button);
        oldEye_colorBlock = button.transform.GetChild(0).gameObject;
    }

    private void SelectEyeButton(GameObject button)
    {
        oldEye_colorBlock.SetActive(false);
        button.transform.GetChild(0).gameObject.SetActive(true);
    }
    /*=====================================================================================================================*/

    public void CreateCharacter()
    {

        List<List<DefaultItem>> defaultItemList;
        int[] selected;

        if (gender_male)
        {
            defaultItemList = defaultMaleLists;
            selected = maleSelected;
        }
        else
        {
            defaultItemList = defaultFemaleLists;
            selected = femaleSelected;
        }

        GlobalControl.instance.isMale = gender_male;

        //Assign player name
        if (!SetPlayerName())
            return;
        GlobalControl.instance.playerName = nameInputField.text;

        EquipmentSO equipment = GlobalControl.instance.equipment;

        //Save the currently equipped default items to EquipmentSO
        equipment.defaultEquipment = new DefaultItem[]{
            defaultItemList[hairIndex][selected[hairIndex]],
            defaultItemList[fHairIndex][selected[fHairIndex]],
            defaultItemList[headIndex][selected[headIndex]],
            defaultItemList[bodyIndex][selected[bodyIndex]],
            defaultItemList[handsIndex][selected[handsIndex]],
            defaultItemList[legsIndex][selected[legsIndex]],
            defaultItemList[feetIndex][selected[feetIndex]]
        };

        //Save the colors from materialBlock
        Color[] colorPrefs = new Color[6];
        colorPrefs[0] = matPropBlock.GetColor("_Color_Hair");
        colorPrefs[1] = matPropBlock.GetColor("_Color_Skin");
        colorPrefs[2] = matPropBlock.GetColor("_Color_Scar");
        colorPrefs[3] = matPropBlock.GetColor("_Color_BodyArt");
        colorPrefs[4] = matPropBlock.GetColor("_Color_Eyes");
        colorPrefs[5] = matPropBlock.GetColor("_Color_Stubble");
        equipment.defaultColorPref = colorPrefs;
        
        //Load those colors into equipmentSO's materialBlock
        equipment.SetBlockColors();

        SceneLoader.Load("Town");
    }

    private bool SetPlayerName()
    {
        String name = nameInputField.text;
        if (name.Length <= 1)
        {
            ShowNameWarning("Name cannot be empty and must be more than one character.");
            return false ;
        }
        GlobalControl.instance.playerName = nameInputField.text;
        return true;
    }

    private void ShowNameWarning(string warningMessage)
    {
        nameWarning.text = "*"+warningMessage;
        nameWarning.gameObject.SetActive(true);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
