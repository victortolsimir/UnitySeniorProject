using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueKillGoal : Goal
{
    
    public QuestController.EnemyType uniqueEnemyID;

    public UniqueKillGoal(QuestController.EnemyType enemyID, string description, bool isCompleted, int curAmount, int requiredAmount, string goalType)
    {
        
        this.uniqueEnemyID = enemyID;
        this.description = description;
        this.completed = isCompleted;
        this.currentAmount = curAmount;
        this.requiredAmount = requiredAmount;
        this.goalType = goalType;
    }

    public override void Init()
    {
        base.Init();
    }

    public void UniqueEnemyDied(QuestController.EnemyType enemyID, UniqueID uniquePrefab)
    {
        
        Debug.Log("In unique death");
        //if (QuestController.instance.bossExclusion.exclusions.Contains(uniquePrefab))
        //{
        //    //this.completed = true;
        //    this.currentAmount++;
        //    Debug.Log("Amount " + this.currentAmount);

        //}
                

            
       

        Evaluate();

        
    }
}
