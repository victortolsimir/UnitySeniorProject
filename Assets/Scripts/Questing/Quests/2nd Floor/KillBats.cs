using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBats : Quest
{
    public KillBats()
    {


        QuestController.EnemyType enemyID = QuestController.EnemyType.Bat;
        int curAmount = 0;
        int requireAmount = 4;
        base.questName = "Bats! Bats! Bats!";
        base.description = "I need you to kill " + requireAmount + " bats." ;
        base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key2-5-1");
        base.itemRewards = new List<Item>();

        FillItemRewards();


        base.goals.Add(new KillGoal(enemyID, description, false, curAmount, requireAmount, "kill"));

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
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Weapons/Meshtint Knight Sword"));
    }
}
