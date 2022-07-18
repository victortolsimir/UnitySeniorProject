using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventorySO inventorySO;

    public Transform InventorySystem;

    //delegate is an event you can subscribe different methods to
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    #region Inventory Singleton
    //Create Singleton for Inventory so only one instance exists
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory exists");
            return;
        }
        instance = this;

    }
    #endregion

    public bool AddItem(Item item)
    {

        if (inventorySO.totalWeight >= inventorySO.weightLimit)
        {
            MessagePanelSystem.ShowMessage("Inventory is too heavy. You can't hold anymore items.");
            return false;
        }

        inventorySO.totalWeight += item.weight;
        inventorySO.items[item.GetItemType()].Add(item);

        // QuestController.instance.ItemWasCollected(item);

        //Tell InventoryUI to update
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
        
    }

    //Used when purchasing item from store
    public void PurchaseItems(Item item, int quantity, int totalCost)
    {
        inventorySO.totalWeight += (item.weight * quantity);

        /*int totalCost = Store.instance.GetCost(item.SellValue) * quantity;*/
        inventorySO.gold -= totalCost;

        for (int i = 0; i < quantity; i++)
        {
            inventorySO.items[item.GetItemType()].Add(item);
        }

        //Tell InventoryUI to update
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }



    }

    public void AddItemFromEquipSlot(Item item)
    {

        inventorySO.items[item.GetItemType()].Add(item);

        //Tell InventoryUI to update
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void RemoveItem(string type, int index)
    {

        Item item = inventorySO.items[type][index];
        inventorySO.totalWeight -= item.weight;

        if (item.stackable)
            index = inventorySO.items[type].LastIndexOf(item);

        inventorySO.items[type].RemoveAt(index);

        //Tell InventoryUI to update
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void RemoveItem(Item item)
    {
        inventorySO.totalWeight -= item.weight;
        inventorySO.items[item.GetItemType()].Remove(item);

        //Tell InventoryUI to update
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void AdjustInventoryWeight(double adjustment)
    {
        inventorySO.totalWeight += adjustment;
    }




}
