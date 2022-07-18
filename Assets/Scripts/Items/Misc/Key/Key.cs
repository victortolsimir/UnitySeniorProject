using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Misc/Key")]
public class Key : Misc
{
    [SerializeField]

    //overrides item's DropItemObject method
    internal override void DropItemObject(Vector3 spawnPos)
    {
        Transform newObject = Instantiate(prefab.transform, spawnPos, Quaternion.identity);
        newObject.name = prefab.name;
        
        //assign this SO to to item pickup
        newObject.GetComponent<ItemPickup>().item = this;
    }

    public override void Use()
    {
        base.Use();
    }

}
