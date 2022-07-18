using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Unique Kill Quest")]
public class UniqueKillQuestSO : QuestSO
{
    public TestUniqueKillGoal goal;

    public override void EnemyKilled(QuestController.EnemyType enemyType) 
    {
        if (goal.IsComplete())
            QuestTrackerUI.UpdateUI("Quest Complete. Return to Jakan.");
    }

    public override bool IsComplete() => goal.IsComplete();

    public override string GetQuestDescription()
    {
        return goal.Description;
    }
}
