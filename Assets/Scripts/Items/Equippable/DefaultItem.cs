using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefaultType {Hair,FacialHair,Head,Body,Hands,Legs,Feet}

[CreateAssetMenu(fileName ="New Default_Item",menuName ="Item/Equippable/Default Item")]
public class DefaultItem : ScriptableObject
{
    public Transform meshObject;
    public DefaultType type;

    public void Equip()
    {
        foreach(Transform itemPiece in meshObject)
        {
            Transform item = FindItem(itemPiece.name);
            //set all skinned meshes of item in scene to enabled
            item.GetComponent<SkinnedMeshRenderer>().enabled = true;
            
            //override shader colors with MaterialPropertyBlock
            MaterialPropertyBlock propertyBlock = GlobalControl.instance.equipment.materialBlock;
            if(propertyBlock != null)
                UpdateItemColorsOnPlayer(GlobalControl.instance.equipment.materialBlock);
            
        }
    }

    public void UnEquip()
    {
        foreach(Transform itemPiece in meshObject)
        {
            Transform item = FindItem(itemPiece.name);
            //set all skinned meshes of item in scene to enabled
            item.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }

    public void UpdateItemColorsOnPlayer(MaterialPropertyBlock block)
    {
        foreach (Transform itemPiece in meshObject)
        {
            Transform item = FindItem(itemPiece.name);
            item.GetComponent<Renderer>().SetPropertyBlock(block);
        }
    }

    private Transform FindItem(string name)
    {
        GameObject parent = GameObject.Find("Player");
        var children = parent.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == name)
                return child;
        return null;
    }
}


