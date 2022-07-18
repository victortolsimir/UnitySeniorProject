using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheTreant : Quest
{
    public KillTheTreant()
    {


        QuestController.EnemyType enemyID = QuestController.EnemyType.BossTreant;
        int curAmount = 0;
        int requireAmount = 1;
        base.questName = "I am Grut";
        base.description = "Use the key to unlock the door the the so called Grut and defeat him";
        base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key2-4-1");
        base.itemRewards = new List<Item>();

        FillItemRewards();

        base.goals.Add(new UniqueKillGoal(enemyID, description, false, curAmount, requireAmount, "uniquekill"));

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
