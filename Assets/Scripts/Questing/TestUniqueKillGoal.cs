using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestUniqueKillGoal : TestGoal
{
    [SerializeField]
    private string _description;
    public string Description { get => _description; }

    [SerializeField]
    private UniqueID _enemyID;
    public UniqueID EnemyID { get => _enemyID; }

    [SerializeField]
    private ExclusionList bossExclusions;

    public override bool IsComplete() => bossExclusions.exclusions.Contains(EnemyID);
}
