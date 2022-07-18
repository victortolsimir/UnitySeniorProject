using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField]
    private LootTable table;

    [SerializeField]
    private GameObject lootBagPrefab;

    private void Start()
    {
        if (GlobalControl.instance.battleInfo.wonBattle)
        {
            GlobalControl.instance.battleInfo.wonBattle = false;

            //get item from drop table
            // Item item = table.GetItemFromTable();
            // var items = table.GetItemsFromTable();
            var items = GlobalControl.instance.battleInfo.enemyLootTable.GetItemsFromTable();

            if (items.Count < 1) return;

            //drop the item in scene below player coordinates
            // item.DropItemObject(GlobalControl.instance.battleInfo.enemyPosition);
            var lootBag = Instantiate(lootBagPrefab, GlobalControl.instance.battleInfo.enemyPosition, Quaternion.identity).GetComponent<LootBag>();
            lootBag.Items = items;

        }
    }
}
