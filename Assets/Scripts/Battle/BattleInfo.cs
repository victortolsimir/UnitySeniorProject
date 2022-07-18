using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/BattleInfo")]
public class BattleInfo : ScriptableObject
{
    private UniqueID enemyID;
    private GameObject enemyPrefab;
    private ExclusionList enemyList;
    public LootTable enemyLootTable { get; private set; }

    public bool wonBattle = false;
    public Vector3 enemyPosition;

    public void EnemyEncountered(UniqueID id, ExclusionList exclusionList, GameObject prefab, LootTable lootTable)
    {
        enemyID = id;
        enemyList = exclusionList;
        enemyPrefab = prefab;
        enemyLootTable = lootTable;
    }

    public void EnemyDefeated()
    {
        wonBattle = true;
        enemyList.Add(enemyID);
    }

    public GameObject GetEnemy() => enemyPrefab;

    public QuestController.EnemyType GetEnemyID()
    {
        return enemyID.enemyType;
    }
    
    public UniqueID GetUniqueID()
    {
        return this.enemyID;
    }
}
