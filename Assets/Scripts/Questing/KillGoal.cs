using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public  class KillGoal : Goal
{
    public QuestController.EnemyType enemyID;
    
    public KillGoal(QuestController.EnemyType enemyID, string description, bool completed, int currentAmount, int requiredAmount, string goalType)
    {
        //this.quest = quest;
        this.enemyID = enemyID;
        this.description = description;
        this.completed = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
        this.goalType = goalType;
    }

    public override void Init()
    {
        base.Init();
    }



    public void EnemyDied(QuestController.EnemyType enemyInfo)
    {
        if(enemyInfo == this.enemyID)
        {
            this.currentAmount++;

            Debug.Log("current amount " + this.currentAmount);
            Evaluate();
            
        }
    }
}
