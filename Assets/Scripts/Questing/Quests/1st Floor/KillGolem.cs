using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGolem : Quest
{
    public KillGolem()
    {
        base.questName = "Defeat The Gate Guarding Golem";
        base.description = "Use the Key I gave you and unlock the far north door and defeat the guardian";
        //base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key1-3-1");
        base.itemRewards = new List<Item>();

        FillItemRewards();

        QuestController.EnemyType enemyID = QuestController.EnemyType.BossGolem;
        int curAmount = 0;
        int requireAmount = 1;

        
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
