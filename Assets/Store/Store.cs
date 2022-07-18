using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public StoreOwner owner;

    public InventoryOptionsPanel inventoryOptions;
    public GameObject inventoryInterface;
    public GameObject storeInterface;

    [Header("Store attributes")]
    public Image StoreIcon;
    public Text StoreName;
    public GameObject Prefab_StoreSlot;
    public Transform ItemContainer;
    public Text PlayerGold;
    public Text PlayerWeight;

    [Header("Item Info Panel")]
    public Image ItemIcon;
    public Text ItemName;
    public Text Description;
    public Text Cost;
    public Text Stats;
    public Text Weight;

    [Header("Total Cost attributes")]
    public Text TotalCost;
    public InputField quantityField;

    [Header("Warning Panel")]
    public GameObject WarningPanel;
    public Text warning;

    [Header("Item Cost Adjustment")]
    [SerializeField]
    private double costAdjustment = 1.60;

    private StoreSlot current;
    private int quantity = 1;

    
    /*======================================================================================================================*/
    public static Store instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Store exists");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            storeInterface.SetActive(false);
        }
    }
    /*======================================================================================================================*/
    public void OpenSellMode()
    {
        owner.StoreDialogueBox.SetActive(false);
        inventoryOptions.ChangeMode(Mode.Store);
        inventoryInterface.SetActive(true);
    }

    public void OpenStore()
    {
        owner.StoreDialogueBox.SetActive(false);
        StoreIcon.sprite = owner.StoreIcon;
        StoreName.text = owner.StoreName;
        CleanStore();
        StockStore();
        UpdatePlayerUI();
        storeInterface.SetActive(true);


    }

    public void CloseOptionsPanel()
    {
        owner.StoreDialogueBox.SetActive(false);
    }

    /*======================================================================================================================*/
    private void CleanStore()
    {
        foreach (Transform child in ItemContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        quantity = 1;
        quantityField.text = "1";
        WarningPanel.SetActive(false);
    }
    private void StockStore()
    {
        for (int i = 0; i < owner.storeItems.Count; i++)
        {
            Transform slotPrefab = Instantiate(Prefab_StoreSlot.transform, ItemContainer.transform);
            StoreSlot newSlot = slotPrefab.GetComponent<StoreSlot>();
            newSlot.AddItem(owner.storeItems[i]);
            if (i == 0)
                ToggleSlot(newSlot);

        }

    }


    /*======================================================================================================================*/
    public void ToggleSlot(StoreSlot slot)
    {
        UnToggleCurrent();
        current = slot;
        slot.SetToggledBorder();
        ItemIcon.sprite = slot.item.icon;
        ItemName.text = slot.item.name;
        Description.text = slot.item.description;
        Cost.text = "Price: " + String.Format("{0:n0}", GetCost(slot.item.SellValue)) + " gp";
        Weight.text = GetItemWeight(slot.item.weight);
        Stats.text = GetStats(slot.item);
        UpdateQuantity("1");
    }

    private void UnToggleCurrent()
    {
        if (current != null)
            current.UnToggle();
    }

    /*======================================================================================================================*/
    private string GetTotalCost()
    {
        return String.Format("{0:n0}", GetCost(current.item.SellValue) * quantity) + $"<color=yellow> gp</color>";
    }

    private string GetStats(Item item)
    {
        string restores = $"<color=lime>{item.GetRestores()}</color>";
        Stats.text = restores;

        string stats = item.GetStats();
        return Stats.text + stats;
    }

    private string GetItemWeight(double weight)
    {
        if (weight == 0)
            return "None";
        return weight.ToString()+"  [Total Qty:  " + (current.item.weight * quantity) + " ]";
    }

    public int GetCost(int sellValue)
    {
        return (int)(sellValue * costAdjustment);
    }

    /*======================================================================================================================*/


    public void UpdateQuantity(String desiredQTY)
    {
        quantity = Int32.Parse(desiredQTY);
        if (quantity == 0)
            quantity = 1;
        quantityField.text = quantity.ToString();
        TotalCost.text = GetTotalCost();
        Weight.text = GetItemWeight(current.item.weight);
    }

    public void AdjustQuantity(string direction)
    {
        switch (direction)
        {
            case "Left":
                AdjustQuantity(-1);
                break;
            case "Right":
                AdjustQuantity(1);
                break;
        }
        TotalCost.text = GetTotalCost();
        Weight.text = GetItemWeight(current.item.weight);
    }

    private void AdjustQuantity(int adjustment)
    {
        int newQTY = quantity + adjustment;
        if (newQTY <= 0 || newQTY > 999)
            quantity = 1;
        else
        {
            quantity = newQTY;
        }
        quantityField.text = quantity.ToString();
    }

    /*======================================================================================================================*/
    public void BuyItem()
    {
        double addedWeight = current.item.weight * quantity;
        double weightResult = Inventory.instance.inventorySO.totalWeight + addedWeight;
        double weightLimit = Inventory.instance.inventorySO.weightLimit;

        if(weightResult > weightLimit)
        {
            warning.text = "Cannot buy that many.  Item quantity exceeds your weight limit.";
            WarningPanel.SetActive(true);
            return;
        }

        int playerGold = Inventory.instance.inventorySO.gold;
        int totalCost = GetCost(current.item.SellValue) * quantity;

        if (owner.isDeveloperStore)
        {
            totalCost = 0;
        }

        if(totalCost > playerGold)
        {
            warning.text = "You don't have enough gold pieces to purchase that.";
            WarningPanel.SetActive(true);
            return;
        }

        //Purchase item(s)
        Inventory.instance.PurchaseItems(current.item, quantity, totalCost);
        AmbientAudio.PlayCoinSound();

        UpdatePlayerUI();

        WarningPanel.SetActive(false);
    }

    private void UpdatePlayerUI()
    {
        //Update inventory weight and player's gold amount
        PlayerGold.text = "Gold: " + String.Format("{0:n0}", Inventory.instance.inventorySO.gold);
        PlayerWeight.text = $"Weight:  {String.Format("{0:0.0}", Inventory.instance.inventorySO.totalWeight)} / {Inventory.instance.inventorySO.weightLimit}";
    }

    /*======================================================================================================================*/

    public void Exit()
    {
        storeInterface.SetActive(false);
    }

}
