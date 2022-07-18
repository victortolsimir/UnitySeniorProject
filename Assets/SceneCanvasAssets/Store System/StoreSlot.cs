using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    public Image icon;
    public Item item;
    public GameObject toggleBorder;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;

    }

    public void SetToggledBorder()
    {
        toggleBorder.SetActive(true);
    }

    public void UnToggle()
    {
        toggleBorder.SetActive(false);
    }

    public void Toggle()
    {
        Store.instance.ToggleSlot(this);
    }


}
