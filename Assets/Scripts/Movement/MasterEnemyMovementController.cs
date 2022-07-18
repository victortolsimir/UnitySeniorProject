using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemyMovementController : MonoBehaviour
{
    private static MasterEnemyMovementController instance;

    private List<EnemyMovement> enemyMovements = new List<EnemyMovement>();

    private bool movement = true;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void ToggleMovement()
    {
        if (instance)
        {
            instance.OnToggleMovement();
        }
    }

    private void OnToggleMovement()
    {
        enemyMovements.ForEach(enemy =>
        {
            enemy.enabled = !movement;
        });

        if (movement)
            GameObject.Find("Player").GetComponent<MyCharacterController>().DisableMovement();
        else
            GameObject.Find("Player").GetComponent<MyCharacterController>().EnableMovement();

        movement = !movement;
    }

    public static void AddEnemy(EnemyMovement enemyMovement)
    {
        if (instance)
            instance.enemyMovements.Add(enemyMovement);
    }
}
