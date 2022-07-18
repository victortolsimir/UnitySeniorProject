using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill3rdFloorMonsters : Quest
{


    public Kill3rdFloorMonsters()
    {
        base.questName = "Kill! Kill! Kill! Kill!";
        base.description = "I need you to kill many enemies";
        base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key3-4-1");
        base.itemRewards = new List<Item>();

        FillItemRewards();

        QuestController.EnemyType cobraID = QuestController.EnemyType.Beetle;
        QuestController.EnemyType spiderID = QuestController.EnemyType.Spider;
        QuestController.EnemyType treantID = QuestController.EnemyType.Treant;
        QuestController.EnemyType batID = QuestController.EnemyType.Bat;
        int curAmount = 0;
        int requireAmount = 4;

        base.goals.Add(new KillGoal(cobraID, "Kill " + requireAmount + " Cobras", false, curAmount, requireAmount, "kill"));

        requireAmount = 2;

        base.goals.Add(new KillGoal(spiderID, "Kill " + requireAmount + " Spiders", false, curAmount, requireAmount, "Kill"));

        requireAmount = 2;

        base.goals.Add(new KillGoal(treantID, "Kill " + requireAmount + " Treants", false, curAmount, requireAmount, "Kill"));
        
        requireAmount = 1;

        base.goals.Add(new KillGoal(batID, "Kill " + requireAmount + " Bat", false, curAmount, requireAmount, "Kill"));



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
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Helmet/Mithril Elvish Helmet"));
        itemRewards.Add((Item)Resources.Load("Items/Consumable/Potions/Greater Health Potion"));
        itemRewards.Add((Item)Resources.Load("Items/Consumable/Potions/Greater Stamina Potion"));
    }
}
