using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsListValuePair
{
    [SerializeField]
    private ItemsListSO _itemsListSO;
    public ItemsListSO ItemsListSO { get => _itemsListSO; }

    [SerializeField, Range(0,100)]
    private int _value;
    public int Value { get => _value; }
}
