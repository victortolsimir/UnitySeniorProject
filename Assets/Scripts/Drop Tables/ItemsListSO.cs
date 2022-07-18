using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemsListSO")]
public class ItemsListSO : ScriptableObject
{
    [SerializeField]
    private List<Item> _items;
    public List<Item> Items { get => _items; }
}
