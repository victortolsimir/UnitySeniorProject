using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemySight), typeof(NavMeshAgent), typeof(WalkForward))]

/*
 * Need to add path distance calculations to ensure the path to the destination doesn't have a distance greater than the patrol radius
 * Need to find issue of some enemies getting stuck
*/
public class EnemyMovement : MonoBehaviour
{
    private float patrolRadius;
    // private float startingRadius;
    private float maxRadius;
    private GameObject target;
    private NavMeshAgent agent;
    private Vector3 startPos, walkPoint;
    private bool walkPointSet = false, pathValid;
    // public LayerMask groundMask;
    private NavMeshPath path;
    // private float totalPathLength;
    private EnemySight enemySight;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        patrolRadius = 6f;
        // startingRadius = 1f;
        maxRadius = patrolRadius * 1.5f;
        startPos = transform.position;
        path = new NavMeshPath();
        target = GameObject.Find("Player");
        enemySight = GetComponent<EnemySight>();
    }

    //private void Start()
    //{
    //    agent.enabled = true;
    //}

    private void Update()
    {
        if (enemySight.PlayerInSight)
        {
            bool isAPath = agent.CalculatePath(target.transform.position, path);
            if (isAPath && Vector3.Distance(startPos, transform.position) <= maxRadius)
            {
                agent.SetDestination(target.transform.position);
            }
            else
            {
                agent.SetDestination(walkPoint);
            }
        }
        else
        {
            Patrolling();
        }
    }

    private void OnEnable()
    {
        agent.enabled = true;
        GetComponent<WalkForward>().StartWalk();
    }

    private void OnDisable()
    {
        agent.enabled = false;
        GetComponent<WalkForward>().StopWalk();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Gizmos.DrawLine(transform.position, transform.forward);
        if (target)
            Gizmos.DrawLine(transform.position, target.transform.position);
        Gizmos.DrawWireSphere(startPos, patrolRadius);
        // Gizmos.DrawWireSphere(startPos, startingRadius);
    }

    private void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        Vector3 distanceFromStart = transform.position - startPos;

        if (distanceToWalkPoint.magnitude < 1f || distanceFromStart.magnitude > patrolRadius || !pathValid)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float totalPathLength;
        do
        {
            float randomZ = Random.Range(-patrolRadius + 1, patrolRadius - 1);
            float randomX = Random.Range(-patrolRadius + 1, patrolRadius - 1);

            walkPoint = new Vector3(startPos.x + randomX, startPos.y, startPos.z + randomZ);
            pathValid = agent.CalculatePath(walkPoint, path);
            totalPathLength = PathDistance(path, walkPoint);
        }
        while (!pathValid || totalPathLength > patrolRadius);

        walkPointSet = true;
    }

    private float PathDistance(NavMeshPath thePath, Vector3 targetPoint)
    {
        Vector3[] allPaths = new Vector3[thePath.corners.Length + 2];

        allPaths[0] = transform.position;
        allPaths[allPaths.Length - 1] = targetPoint;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allPaths[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allPaths.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allPaths[i], allPaths[i + 1]);
        }

        return pathLength;
    }
}
