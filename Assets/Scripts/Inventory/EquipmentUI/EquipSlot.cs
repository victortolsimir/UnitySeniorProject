using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public Equippable item;
    public Image icon;
    public Sprite defaultIcon;
    public Button slotButton;
    public GameObject removeButton;
    public void AddItem(Equippable newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        slotButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = defaultIcon;
        slotButton.interactable = false;
        removeButton.SetActive(false);
    }

    public void RemoveItem()
    {
        item.UnEquip();
    }

    public void ToggleSlot()
    {
        ToggledInventorySlot current = GameObject.Find("InventoryManager").GetComponent<ToggledInventorySlot>();
        current.currentlyToggled = null;
        current.toggledItem = this.item;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item!= null)
            removeButton.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(item!= null)
            removeButton.SetActive(false);
    }
}
