using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectionTest : Quest
{


    public CollectionTest() 
    {
        questName = "Collection keys";
        description = "keys keys";
        specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key3-4-1");

        List<Item> itemsToCollect = new List<Item>();
        itemsToCollect.Add((Item)Resources.Load("Items/Misc/Keys/Key1-3-1"));
        itemsToCollect.Add((Item)Resources.Load("Items/Misc/Keys/Key1-2-1"));
        //itemsToCollect.Add((Item)Resources.Load("Items/Misc/Keys/Key1-3-1"));

        goals.Add(new CollectionGoal(this, itemsToCollect, "Collect 2 keys", false, 0, 2, "collect"));

        foreach (Goal g in this.goals)
        {
            g.Init();
        }
    }

    public override void GiveReward(LootBag lootBag)
    {
        base.GiveReward(lootBag);
        Inventory.instance.AddItem(specialItemReward);
    }
}
