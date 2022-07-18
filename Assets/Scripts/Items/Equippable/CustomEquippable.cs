using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Equippable/Custom")]
public class CustomEquippable : Armor
{
    [Header("Custom Item Fields")]
    public GameObject mesh;
    public string parentBone;

    public override string GetItemType() => "Armor";
    
}
