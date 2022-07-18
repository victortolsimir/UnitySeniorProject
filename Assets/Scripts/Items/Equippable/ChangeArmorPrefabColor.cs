using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArmorPrefabColor : MonoBehaviour
{
    public ArmorColorBlock armorColor;

    private void Awake()
    {
        foreach(Transform item in transform)
        {
            item.GetComponent<Renderer>().SetPropertyBlock(armorColor.GetMaterialPropertyBlock());
        }
    }

}
