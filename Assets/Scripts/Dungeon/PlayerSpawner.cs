using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private UniqueID spawnID;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (spawnID == GlobalControl.instance.spawnID)
        {
            GlobalControl.instance.spawnID = null;
            GameObject.Find("Main Camera").transform.position = transform.position;
            player.position = transform.position;
        }
    }
}
