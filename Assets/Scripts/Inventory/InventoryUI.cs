using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public Transform WeaponItemsParent;
    public Transform FoodItemsParent;
    public Transform ArmorItemsParent;
    public Transform PotionItemsParent;
    public Transform MiscItemsParent;

    public Transform prefabInventorySlot;
    public GameObject inventoryUI;
    public InventoryOptionsPanel optionsPanel;

    public Text inventoryWeightField;
    public Text coinField;

    Inventory inventory;

    Dictionary<string, InventorySlot[]> slots = new Dictionary<string, InventorySlot[]>();
    Dictionary<string, Transform> itemParents;


    void Start()
    {
        itemParents = new Dictionary<string, Transform>()
        {
            {"Weapon",WeaponItemsParent},{"Food",FoodItemsParent},{"Armor",ArmorItemsParent },{"Potion",PotionItemsParent },{"Misc",MiscItemsParent}
        };

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        UpdateUI();
        DisplayTotalWeight();
        
    }

    private void UpdateSlots()
    {

        if (slots.Count == 0)
        {
            slots.Add("Weapon", WeaponItemsParent.GetComponentsInChildren<InventorySlot>());
            slots.Add("Food", FoodItemsParent.GetComponentsInChildren<InventorySlot>());
            slots.Add("Armor", ArmorItemsParent.GetComponentsInChildren<InventorySlot>());
            slots.Add("Potion", PotionItemsParent.GetComponentsInChildren<InventorySlot>());
            slots.Add("Misc", MiscItemsParent.GetComponentsInChildren<InventorySlot>());
        }

        slots["Weapon"]= WeaponItemsParent.GetComponentsInChildren<InventorySlot>();
        slots["Food"] = FoodItemsParent.GetComponentsInChildren<InventorySlot>();
        slots["Armor"] = ArmorItemsParent.GetComponentsInChildren<InventorySlot>();
        slots["Potion"] = PotionItemsParent.GetComponentsInChildren<InventorySlot>();
        slots["Misc"] = MiscItemsParent.GetComponentsInChildren<InventorySlot>();

    }

    void Update()
   {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            ChangeOptionsPanelMode();
        }
        else if (Input.GetKeyDown("escape"))
        {
            inventoryUI.SetActive(false);
            ChangeOptionsPanelMode();
        }
    }

    private void ChangeOptionsPanelMode()
    {
        if (!inventoryUI.activeSelf)
            optionsPanel.ChangeMode(Mode.Inventory);
    }

    //method subscribed on onItemChangedCallback delegate
    void UpdateUI()
    {
        UpdateSlots();

        

        //go through each tab and fill with its item type
        foreach (KeyValuePair<string, InventorySlot[]> tab in slots)
        {
            Dictionary<Item, int> stackableItemIndex = new Dictionary<Item, int>();
            int itemsInTabCount = inventory.inventorySO.items[tab.Key].Count;
            int slotCount = tab.Value.Length;
            int filled = 0;

            for (int i = 0; i < itemsInTabCount; i++) //while we still have items to assign
            {
                Item item = inventory.inventorySO.items[tab.Key][i];
                if(item.stackable)
                {
                    if(!stackableItemIndex.ContainsKey(item) && filled == slotCount)
                    {
                        InventorySlot newSlot = AddInventorySlot(tab,item,filled);
                        filled++;
                        slotCount++;
                    }
                    else if(!stackableItemIndex.ContainsKey(item)) //item doesn't exist yet in an inventoryslot
                    {
                        tab.Value[filled].AddItem(item);
                        tab.Value[filled].itemIndex = i;
                        stackableItemIndex.Add(item, filled);
                        filled++;
                        
                    }
                    else //item already has a slot
                    {
                        int itemIndex = stackableItemIndex[item];
                        InventorySlot slot = tab.Value[itemIndex];
                        slot.UpdateCount(1);
                    }
                }
                else
                {
                    if(filled == slotCount)
                    {
                        AddInventorySlot(tab, item, filled);
                        filled++;
                        slotCount++;
                    }
                    else
                    {
                        tab.Value[filled].AddItem(item);
                        tab.Value[filled].itemIndex = i;
                        filled++;
                    }
                }

            }
            RemoveExcessSlots(filled, tab.Value);
        }

        DisplayTotalWeight();
        DisplayCoins();

    }

    private InventorySlot AddInventorySlot(KeyValuePair<string, InventorySlot[]> tab, Item item, int slotIndex)
    {
        //add an Inventory Slot
        Transform newSlot = Instantiate(prefabInventorySlot, itemParents[tab.Key]);
        newSlot.name = "InventroySlot (" + slots[tab.Key].Length + ")";
        newSlot.GetComponent<InventorySlot>().AddItem(item);
        newSlot.GetComponent<InventorySlot>().itemIndex = slotIndex;

        return newSlot.GetComponent<InventorySlot>();
    }

    private void RemoveExcessSlots(int filled, InventorySlot[] tabSlots)
    {
        for(int i = filled; i < tabSlots.Length; i++)
        {
            if (tabSlots[i].item = null) return;
            if (i < 24) {
                tabSlots[i].ClearSlot();
            }
            else
            {
                //remove slot object
                InventorySlot slot = tabSlots[i];
                Destroy(slot.gameObject);
            }
        }
    }

    private void DisplayTotalWeight()
    {
            inventoryWeightField.text = $"Carry Weight: {String.Format("{0:0.0}", inventory.inventorySO.totalWeight)} / {inventory.inventorySO.weightLimit}";
            if (inventory.inventorySO.totalWeight >= inventory.inventorySO.weightLimit)
            {
                inventoryWeightField.color = Color.red;
            }
            else
            {
                inventoryWeightField.color = Color.white;
            }
    }

    private void DisplayCoins()
    {
        coinField.text = "Gold: "+inventory.inventorySO.gold.ToString();
    }

    public void OpenInventory()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            ChangeOptionsPanelMode();
        }

    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        ChangeOptionsPanelMode();
    }



}
