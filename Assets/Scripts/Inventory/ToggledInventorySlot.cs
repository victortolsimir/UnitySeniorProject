using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggledInventorySlot : MonoBehaviour
{
    public GameObject inventoryInterface;

    public InventorySlot currentlyToggled { get; set; }
    public int index { get { return currentlyToggled.itemIndex; } private set { } }

    public Item toggledItem;

    public static ToggledInventorySlot instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CurrentToggledInventorySlot exists");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if(currentlyToggled != null)
            toggledItem = currentlyToggled.item;
            
        if (toggledItem != null)
            DisplayItemInfo();
    }

    public void DisplayItemInfo()
    {
        GameObject itemInfoContainer = inventoryInterface.transform.Find("ItemInfoContainer").gameObject;

        if (inventoryInterface.activeSelf)
        {
            itemInfoContainer.SetActive(true);

            Item item = toggledItem;

            if(item != null)
            {
                GameObject itemImage = GameObject.Find("ItemInfoContainer/ItemImage").gameObject;
                itemImage.GetComponent<Image>().sprite = item.icon;

                GameObject itemTitle = GameObject.Find("ItemInfoContainer/ItemTitle").gameObject;
                itemTitle.GetComponent<Text>().text = item.name;

                GameObject itemWeight = GameObject.Find("ItemInfoContainer/DescriptionContainer/Weight").gameObject;
                itemWeight.GetComponent<Text>().text = $"Weight: {string.Format("{0:0.0}", item.weight)}";

                GameObject itemDescription = GameObject.Find("ItemInfoContainer/DescriptionContainer/ItemDescription").gameObject;
                itemDescription.GetComponent<Text>().text = item.description;

                GameObject sellValue = GameObject.Find("ItemInfoContainer/DescriptionContainer/SellValue").gameObject;
                string itemSellValue = item.SellValue.ToString("N0");
                sellValue.GetComponent<Text>().text = $" <color=#F3C10B>{itemSellValue}</color>";

                //Display Item Stat Info

                GameObject itemStats = GameObject.Find("ItemInfoContainer/ItemStatsContainer/Stats").gameObject;
                Text statInfo = itemStats.GetComponent<Text>();
                
                string restores = $"<color=lime>{item.GetRestores()}</color>";
                statInfo.text = restores;

                string stats = item.GetStats();
                statInfo.text = statInfo.text + stats;

            }

            
        }
        else
        {
            itemInfoContainer.SetActive(false);

        }
    }
}
