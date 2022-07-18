using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBeetles : Quest
{


    public KillBeetles() 
    {     
        base.questName = "Kill Beetles";
        base.description = "I need you to Kill 4 beetles and then return to me";
        base.specialItemReward = (Item)Resources.Load("Items/Misc/Keys/Key1-3-1");
        base.itemRewards = new List<Item>();
        FillItemRewards();
        base.goldReward = 100;

        QuestController.EnemyType enemyID = QuestController.EnemyType.Beetle;
        int curAmount = 0;
        int requireAmount = 4;

        base.goals.Add(new KillGoal(enemyID, description, false, curAmount, requireAmount, "kill"));
        
        foreach(Goal g in this.goals)
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
        itemRewards.Add((Item)Resources.Load("Items/Consumable/Food/Apple"));
    }

}
