using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillKingCobra : Quest
{
    public KillKingCobra()
    {
        base.questName = "Kill the King";
        base.description = "Kill the King Cobra";
        base.itemRewards = new List<Item>();

        FillItemRewards();

        QuestController.EnemyType enemyID = QuestController.EnemyType.BossCobra;
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
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Chest/Leather PlateBody"));
    }
}
