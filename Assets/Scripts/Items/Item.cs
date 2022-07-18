using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public abstract class Item : ScriptableObject
{
    //new overrides old definition
    new public string name = "New Item";
    public Sprite icon = null;
    public GameObject prefab;
    public double weight = 0;
    public string description = "";
    public int SellValue;
    public bool stackable;

    [Header("Disable Options")]
    public bool disableUse;
    public bool disableDrop;
    

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public abstract string GetItemType();
    public abstract string UseAdjective();

    internal virtual void DropItemObject(Vector3 spawnPos)
    {
        Transform newObject = Instantiate(prefab.transform, spawnPos, Quaternion.identity);
        newObject.name = prefab.name;
    }

    public virtual string GetRestores()
    {
        return "";
    }

    public virtual string GetStats()
    {
        return "";
    }


}
