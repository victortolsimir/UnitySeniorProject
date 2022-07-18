using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text weight;
    public TextMeshProUGUI stackCount;
    public Button inventorySlotButton;
    public Item item;
    public int itemIndex;
    public int count;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        weight.text = item.weight.ToString("0.0");
        count = 1;
        stackCount.SetText("");
        icon.enabled = true;
        inventorySlotButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        itemIndex = -1;
        icon.sprite = null;
        icon.enabled = false;
        weight.text = "";
        stackCount.SetText("");
        inventorySlotButton.interactable = false;
    }

    public void UpdateCount(int add)
    {
        count += add;
        stackCount.SetText(count.ToString());
    }

    public void ButtonClicked()
    {
        SetToggledSlot();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            InventoryOptionsPanel optionsPanel = GameObject.Find("InventorySystem").GetComponent<InventoryUI>().optionsPanel;
            if (optionsPanel.mode == Mode.Inventory)
                UseItem();
            if (optionsPanel.mode == Mode.Store)
                optionsPanel.Sell();
        }
    }

    public void SetToggledSlot()
    {
        GameObject.Find("InventoryManager").GetComponent<ToggledInventorySlot>().currentlyToggled = this;
    }

    internal void DropItemObject()
    {
        item.DropItemObject(GameObject.Find("Player").transform.position);
    }

    public void UseItem()
    {
        item.Use();
        if(item)
        {
            //remove consumables from this slot after use
            if (item is Consumable || item is SpellBook)
            {
                Inventory.instance.RemoveItem(item.GetItemType(), itemIndex);
            }
        }
    }
}
