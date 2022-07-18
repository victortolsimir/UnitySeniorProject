using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreFloor1 : Quest
{ 
    public ExploreFloor1()
    {
        base.questName = "Explore and find Kev";
        base.description = "Use the Key I gave you and explore the dungeon to find Kev";
        base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key1-2-1");
        base.itemRewards = new List<Item>();

        FillItemRewards();

        QuestController.EnemyType enemyID = QuestController.EnemyType.GiantChicken;
        int curAmount = 0;
        int requireAmount = 1;

        base.goals.Add(new UniqueKillGoal(enemyID,description, false, curAmount, requireAmount, "uniquekill"));

        foreach (Goal g in this.goals)
        {
            g.Init();
        }

    }

    public override void GiveReward(LootBag lootBag)
    {
        base.GiveReward(lootBag);
    }

    private void FillItemRewards()
    {
        itemRewards.Add((Item)Resources.Load("Items/Consumable/Food/Carrot"));
    }

}
