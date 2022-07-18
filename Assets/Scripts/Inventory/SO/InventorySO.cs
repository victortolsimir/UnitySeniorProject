using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory")]
public class InventorySO : ScriptableObject
{
    public Dictionary<string, List<Item>> items = new Dictionary<string, List<Item>>
    {
        {"Weapon",new List<Item>()},
        {"Food",new List<Item>()},
        {"Armor",new List<Item>()},
        {"Potion",new List<Item>()},
        {"Misc",new List<Item>()}
    };

    public double weightLimit;
    public double totalWeight;
    public int gold;

    public void ClearAll()
    {
        totalWeight = 0;
        weightLimit = 300;
        gold = 0;
        foreach (var kvp in items)
        {
            kvp.Value.Clear();
        }
    }
}
