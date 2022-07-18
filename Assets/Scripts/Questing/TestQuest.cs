using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestQuest : Quest
{
    
    
    public TestQuest()
    {

        QuestController.EnemyType enemyID = QuestController.EnemyType.None;
        
        base.questName = "Testing";
        base.description = "Testing testing 1.. 2.. 3";

       
        //goals.Add(new UniqueKillGoal(3, "Kill the golem", false, 0, 1, "uniquekill"));
        //goals.Add(new CollectionGoal(this, "Key", "Collect 3 keys", false, 0, 3, "collect"));
        //goals.Add(new KillGoal(this, 3, "Kill the Guarding Golem", false, 0, 1, "kill"));
        base.goals.Add(new KillGoal(enemyID, description, false, 0, 2, "kill"));
        
        foreach(Goal g in this.goals)
        {
            g.Init();
        }
        
    }

    public override void GiveReward(LootBag lootBag)
    {
        base.GiveReward(lootBag);
    }
}
