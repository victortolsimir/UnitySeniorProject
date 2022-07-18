using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemValuePair
{
    [SerializeField]
    private Item _item;
    public Item Item { get => _item; }

    [SerializeField, Range(0, 100)]
    private int _value;
    public int Value { get => _value; }
}
