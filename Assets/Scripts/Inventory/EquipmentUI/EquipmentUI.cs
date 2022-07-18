using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform EquipmentPanel;

    EquipSlot[] slots;
    EquipmentManager equipManager;

    private void Start()
    {
        equipManager = EquipmentManager.instance;
        equipManager.onEquipChangedCallback += UpdateUI;

        slots = EquipmentPanel.GetComponentsInChildren<EquipSlot>();

        UpdateUI();
    }

    private void UpdateUI()
    {
        Equippable[] currentEquipment = equipManager.equipmentSO.currentEquipment;
        for(int i = 0; i <currentEquipment.Length; i++)
        {
            if(currentEquipment[i] != null)
            {
                slots[i].AddItem(currentEquipment[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
