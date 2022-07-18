using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestGoal
{
    public virtual void EnemyKilled(QuestController.EnemyType enemyType) { }

    public abstract bool IsComplete();

    public virtual void Reset() { }

    public virtual void Load(int amount) { }
}
