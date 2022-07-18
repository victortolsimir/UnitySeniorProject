using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public UniqueID uniqueID;
    public ExclusionList exclusionList;
    [SerializeField]
    protected bool canMove = false;

    private void Start()
    {
        if (!exclusionList.exclusions.Contains(uniqueID))
        {
            var go = Instantiate(prefab, transform);
            if (canMove)
            {
                var movement = go.GetComponent<EnemyMovement>();
                movement.enabled = true;
                MasterEnemyMovementController.AddEnemy(movement);
            }
        }
    }
}
