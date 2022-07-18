using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBagManager : MonoBehaviour
{
    [SerializeField]
    private GameObject lootBagPanel;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private GameObject template;

    private static LootBagManager instance;

    private GameObject selectedButton;

    private Transform itemViewImage;

    private Transform itemInfo;

    private Transform warningPanel;

    private List<Item> Items { get; set; }

    private GameObject lootBag;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of LootBag Manager exists");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        itemViewImage = lootBagPanel.transform.Find("ItemView").Find("image");
        itemInfo = lootBagPanel.transform.Find("ItemInfo");
        warningPanel = lootBagPanel.transform.Find("WarningPanel");
    }

    public static void OpenBag(LootBag lootBag)
    {
        if (instance)
            instance.OnOpenBag(lootBag);
    }

    private void OnOpenBag(LootBag lootBag)
    {
        if (lootBagPanel.activeSelf) return;

        MasterEnemyMovementController.ToggleMovement();

        this.lootBag = lootBag.gameObject;
        Items = lootBag.Items;
        UpdateUI();
        lootBagPanel.SetActive(true);
    }

    public static void CloseBag()
    {
        if (instance)
            instance.OnCloseBag();
    }

    public void OnCloseBag()
    {
        MasterEnemyMovementController.ToggleMovement();

        warningPanel.gameObject.SetActive(false);
        lootBagPanel.SetActive(false);
    }

    public void OnCollectItem()
    {
        if (!selectedButton) return;
        var item = selectedButton.GetComponent<ItemContainer>().Item;

        double remainingWeight = Inventory.instance.inventorySO.weightLimit - Inventory.instance.inventorySO.totalWeight;
        if (item.weight <= remainingWeight)
        {
            Inventory.instance.AddItem(item);
            Items.Remove(item);
            if (Items.Count > 0)
            {
                warningPanel.gameObject.SetActive(false);
                UpdateUI();
            }
            else
            {
                Destroy(lootBag);
                OnCloseBag();
            }
        }
        else
        {
            warningPanel.gameObject.SetActive(true);
            warningPanel.Find("Text").GetComponent<Text>().text = "You can't collect that, it will exceed your weight limit.";
        }
    }

    public void OnCollectAll()
    {
        double totalWeight = 0;
        Items.ForEach(item => { totalWeight += item.weight; });

        double remainingWeight = Inventory.instance.inventorySO.weightLimit - Inventory.instance.inventorySO.totalWeight;
        if (totalWeight <= remainingWeight)
        {
            var inventory = Inventory.instance;
            Items.ForEach(item => inventory.AddItem(item));
            Items.Clear();
            Destroy(lootBag);
            OnCloseBag();
        }
        else
        {
            warningPanel.gameObject.SetActive(true);
            warningPanel.Find("Text").GetComponent<Text>().text = "You can't collect all that, it will exceed your weight limit.";
        }
    }

    private void UpdateUI()
    {
        if (itemViewImage.gameObject.activeSelf)
        {
            itemViewImage.gameObject.SetActive(false);
            itemInfo.gameObject.SetActive(false);
        }
        ClearContent();
        FillContent();
        FillItemInfo();
        selectedButton.transform.GetComponentInChildren<Text>().color = new Color(0.431f, 0.843f, 0.968f);
    }
    private void FillContent()
    {
        int i = 0;
        Items.ForEach(item => 
        {
            
            var button = Instantiate(template, content.transform);
            if (i == 0)
                selectedButton = button; i++;
            button.name = item.name;
            button.GetComponentInChildren<Text>().text = item.name;
            button.GetComponent<ItemContainer>().Item = item;

            button.SetActive(true);
        });
    }

    private void ClearContent()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnItemSelect()
    {
        if (selectedButton != null)
            selectedButton.GetComponentInChildren<Text>().color = Color.white;

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        button.transform.GetComponentInChildren<Text>().color = new Color(0.431f, 0.843f, 0.968f);
        selectedButton = button;
        FillItemInfo();
    }

    private void FillItemInfo()
    {
        if (itemViewImage.gameObject.activeSelf)
        {
            itemViewImage.gameObject.SetActive(false);
            itemInfo.gameObject.SetActive(false);
        }

        var item = selectedButton.GetComponent<ItemContainer>().Item;
        itemViewImage.GetComponent<Image>().sprite = item.icon;

        itemInfo.Find("name").GetComponent<Text>().text = item.name;
        itemInfo.Find("Description").GetComponent<Text>().text = item.description;

        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"Sell value: {item.SellValue}");
        sb.AppendLine($"Weight: {item.weight}");

        itemInfo.Find("stats").GetComponent<Text>().text = sb.ToString();
        itemInfo.gameObject.SetActive(true);
        itemViewImage.gameObject.SetActive(true);
    }
}
