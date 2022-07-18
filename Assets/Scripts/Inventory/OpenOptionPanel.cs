using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenOptionPanel : MonoBehaviour, IPointerClickHandler
{
    public Button button;

    private GameObject optionsPanel;
    private InventorySlot slot;

    private void Start()
    {
        optionsPanel = GameObject.Find("InventorySystem").transform.Find("Options Panel").gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(button.interactable == true)
            {
                //set current Inventory Slot to toggled
                slot = button.transform.parent.GetComponent<InventorySlot>();
                slot.SetToggledSlot();
                
                Item item = button.GetComponentInParent<InventorySlot>().item;
                if(!(item.disableDrop && item.disableUse))
                    ShowOptionPanel();
            }
            else
            {
                Debug.Log("Button disabled. Cannot right click");
            }
        }
    }

    public void ShowOptionPanel()
    {

        optionsPanel.transform.position = Input.mousePosition;
        optionsPanel.GetComponent<InventoryOptionsPanel>().UpdateOptionsPanel();
        optionsPanel.SetActive(true);

        //set optionsPanel use option to currentInventory slot's item 'Use' adjective
        optionsPanel.transform.Find("Use/Text").GetComponent<Text>().text = slot.item.UseAdjective();
    }
}
