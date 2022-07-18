using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EquipmentSO : ScriptableObject
{
    public Equippable[] currentEquipment = new Equippable[System.Enum.GetNames(typeof(EquipmentSlot)).Length];

    [Header("Default Items")]
    public DefaultItem[] defaultEquipment = new DefaultItem[System.Enum.GetNames(typeof(DefaultType)).Length];

    [Header("Static Default Items")]
    public DefaultItem[] staticDefaults;

    public Color[] defaultColorPref;

    public MaterialPropertyBlock materialBlock;

    public void ClearAll()
    {
        //Clear all equipment
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            currentEquipment[i] = null;
        }

        //Clear all default items
        for (int i = 0; i < defaultEquipment.Length; i++)
        {
            defaultEquipment[i] = null;
        }

        defaultColorPref = null;
        materialBlock = null;
    }

    public DefaultItem[] defaultItems()
    {
        if(defaultEquipment[(int)DefaultType.Head] == null)
            return staticDefaults;

        //return new List<DefaultItem>() {Hair, FacialHair, Head, Body, Hands, Legs, Feet};
        return defaultEquipment;
    }

    internal void SetBlockColors()
    {
        if(materialBlock == null)
            materialBlock = new MaterialPropertyBlock();
       
        String[] keys = { "_Color_Hair", "_Color_Skin", "_Color_Scar", "_Color_BodyArt", "_Color_Eyes", "_Color_Stubble" };
        Color nullValue = Color.clear;
        
        for (int i = 0; i < defaultColorPref.Length; i++)
        {
            if (!defaultColorPref[i].Equals(nullValue))
                materialBlock.SetColor(keys[i], defaultColorPref[i]);
            
        }

    }
}
