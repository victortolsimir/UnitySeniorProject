using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestKillGoal : TestGoal
{
    [SerializeField]
    private string _description;
    public string Description { get => $"{_description}: {CurrentAmount} / {RequiredAmount}"; }

    [SerializeField]
    private QuestController.EnemyType _enemyType;
    public QuestController.EnemyType EnemyType { get => _enemyType; }

    [SerializeField]
    private int _currentAmount = 0;
    public int CurrentAmount { get => _currentAmount; private set => _currentAmount = value > RequiredAmount ? RequiredAmount : value; }

    [SerializeField]
    private int _requiredAmount = 0;
    public int RequiredAmount { get => _requiredAmount; }

    public override void EnemyKilled(QuestController.EnemyType enemyType)
    {
        if (enemyType == EnemyType)
        {
            CurrentAmount++;
        }
    }

    public override bool IsComplete() => CurrentAmount >= RequiredAmount;

    public override void Reset() => CurrentAmount = 0;

    public override void Load(int amount) => CurrentAmount = amount;
}
