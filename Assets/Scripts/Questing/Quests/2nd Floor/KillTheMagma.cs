using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheCobra : Quest
{
    public KillTheCobra()
    {


        QuestController.EnemyType enemyID = QuestController.EnemyType.SpecialMagma;
        int curAmount = 0;
        int requireAmount = 1;
        base.questName = "";
        base.description = "Use the key you earned to open the door .";
        base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key2-1-3");
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
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Weapons/Meshtint Knight Sheld"));
    }
}
