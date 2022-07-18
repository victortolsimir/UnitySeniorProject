using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private List<ItemsListValuePair> lootTable;

    [SerializeField]
    private List<ItemValuePair> uniqueLootTable;

    public List<Item> GetItemsFromTable()
    {
        var random = new System.Random();
        var items = new List<Item>();

        lootTable.ForEach(pair => 
        {
            pair.ItemsListSO.Items.ForEach(item => 
            {
                if (random.Next(100) < pair.Value)
                    items.Add(item);
            });
        });

        uniqueLootTable.ForEach(pair =>
        {
            if (random.Next(100) < pair.Value)
                items.Add(pair.Item);
        });

        return items;
    }
}
