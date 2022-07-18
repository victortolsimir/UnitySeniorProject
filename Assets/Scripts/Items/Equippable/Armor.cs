using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equippable/Armor")]
public class Armor : Equippable
{
    public bool isCape;

    [Header("Item Equippable GameObject")]
    public GameObject itemPieces;
    public GameObject femaleItemPieces;

    [Header("Disable Default Item")]
    public bool disable_hair = false;
    public bool disable_facialHair = false;
    public bool disable_head = false;
    public bool disable_body = false;
    public bool disable_hands = false;
    public bool disable_legs = false;
    public bool disable_feet = false;

    [Header("Override original colors")]
    public ArmorColorBlock armorColorBlock;

    public override string GetItemType() => "Armor";

    public bool[] GetDefaultDisables()
    {
        return new bool[] { disable_hair, disable_facialHair, disable_head, disable_body, disable_hands, disable_legs, disable_feet };
    }

}

