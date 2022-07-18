using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Mode {Inventory,Store}
public class InventoryOptionsPanel : MonoBehaviour
{       
    public Mode mode = Mode.Inventory;

    public void ChangeMode(Mode mode)
    {
        DisableMode();
        this.mode = mode;
        EnableMode();
    }

    public void UpdateOptionsPanel()
    {
        bool updated = false;
        
        if (mode == Mode.Store) return;
        Item toggledItem = ToggledInventorySlot.instance.currentlyToggled.item;

        if (toggledItem.disableUse)
        {
            transform.Find("Use").gameObject.SetActive(false);
            updated = true;
        }
 
        if (toggledItem.disableDrop)
        {
            transform.Find("Drop").gameObject.SetActive(false);
            updated = true;
        }
            
        if(!updated)
            ChangeMode(Mode.Inventory);
            


    }

    private void EnableMode()
    {
        switch (mode)
        {
            case Mode.Inventory:
                transform.Find("Use").gameObject.SetActive(true);
                transform.Find("Drop").gameObject.SetActive(true);
                break;
            case Mode.Store:
                transform.Find("Sell").gameObject.SetActive(true);
                break;
        }
        
        
    }

    private void DisableMode()
    {
        switch (mode)
        {
            case Mode.Inventory:
                transform.Find("Use").gameObject.SetActive(false);
                transform.Find("Drop").gameObject.SetActive(false);
                break;
            case Mode.Store:
                transform.Find("Sell").gameObject.SetActive(false);
                break;

        }

        
    }
    
    /*======================================================================================================================================*/
   
    //INVENTORY MODE

    public void UseItem()
    {
        InventorySlot currentlyToggled = ToggledInventorySlot.instance.currentlyToggled;
        currentlyToggled.UseItem();
        Close();
    }

    public void DropItem()
    {
        InventorySlot currentlyToggled = ToggledInventorySlot.instance.currentlyToggled;

        currentlyToggled.DropItemObject();
        Inventory.instance.RemoveItem(currentlyToggled.item.GetItemType(), currentlyToggled.itemIndex);

        Close();
    }

    /*======================================================================================================================================*/
    
    //STORE MODE
    public void Sell()
    {
        InventorySlot currentlyToggled = ToggledInventorySlot.instance.currentlyToggled;
        if(currentlyToggled.item.disableDrop && currentlyToggled.item.disableUse) { return; }
        Inventory.instance.inventorySO.gold += currentlyToggled.item.SellValue;
        Inventory.instance.RemoveItem(currentlyToggled.item.GetItemType(), currentlyToggled.itemIndex);
        AmbientAudio.PlayCoinSound();
        Close();
    }

    /*======================================================================================================================================*/
    public void Close()
    {
        gameObject.SetActive(false);
    }

   
}
