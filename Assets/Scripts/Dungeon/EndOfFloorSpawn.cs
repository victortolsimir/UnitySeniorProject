using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfFloorSpawn : MovePlayerSpawn
{
    protected override void MovePlayer()
    {
        if (GlobalControl.instance.movingToPrevFloor)
        {
            GlobalControl.instance.movingToPrevFloor = false;
            GameObject.Find("Main Camera").transform.position = transform.position;
            player.position = transform.position;
        }
    }
}
