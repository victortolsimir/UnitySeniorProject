using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemySight : MonoBehaviour
{
    public bool PlayerInSight { get; private set; } = false;
    private float fieldOfViewAngle;
    // private GameObject player;
    // private float angleBetween;
    // private float halfOfView;
    // private GameObject whatWasHit;
    // public Vector3 currentPosition;
    // public float enemyCollisionRadius;

    private SphereCollider col;

    private void Awake()
    {
        fieldOfViewAngle = 60f;
        // player = GameObject.Find("Player");
        col = GetComponent<SphereCollider>();
        // halfOfView = fieldOfViewAngle * 0.5f;
    }

    //private void Update()
    //{
    //    currentPosition = transform.position + transform.forward;
    //    col.center = currentPosition;
    //}

    public void OnTriggerStay(Collider other)
    {
        // if (other.gameObject == player)
        if (other.CompareTag("Player"))
        {
            PlayerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            // angleBetween = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                bool aHit = Physics.Raycast(transform.position + transform.up, direction.normalized, out RaycastHit hit, col.radius);

                if (aHit)
                {
                    // whatWasHit = hit.collider.gameObject;

                    // if (hit.collider.gameObject == player)
                    if (hit.collider.CompareTag("Player"))
                    {
                        PlayerInSight = true;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //if (other.gameObject == player)
        if (other.CompareTag("Player"))
            PlayerInSight = false;
    }
}
