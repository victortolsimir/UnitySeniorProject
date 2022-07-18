using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Kill Quest")]
public class KillQuestSO : QuestSO
{
    public List<TestKillGoal> goals = new List<TestKillGoal>();

    public override void Reset()
    {
        goals.ForEach(goal => { goal.Reset(); });
    }

    public override bool IsComplete()
    {
        bool complete = true;
        goals.ForEach(goal => { if (!goal.IsComplete()) complete = false; });

        return complete;
    }

    public override void EnemyKilled(QuestController.EnemyType enemyType)
    {
        goals.ForEach(goal => { goal.EnemyKilled(enemyType); });
        QuestController.instance.questTrackerInfo = GetQuestDescription();
        // QuestTrackerUI.UpdateUI(GetQuestDescription());
    }

    public List<int> GetGoalProgress()
    {
        var list = new List<int>();
        foreach (var goal in goals)
        {
            list.Add(goal.CurrentAmount);
        }

        return list;
    }

    public void SetGoalProgress(List<int> goalProgress)
    {
        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].Load(goalProgress[i]);
        }
    }

    public override string GetQuestDescription()
    {
        if (IsComplete())
            return "Quest Complete. Return to Jakan.";
        var sb = new System.Text.StringBuilder();
        foreach (var goal in goals)
        {
            sb.AppendLine(goal.Description);
        }

        return sb.ToString();
    }
}
