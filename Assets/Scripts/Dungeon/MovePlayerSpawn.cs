using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovePlayerSpawn : MonoBehaviour
{
    protected Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        MovePlayer();
    }

    protected abstract void MovePlayer();
}
