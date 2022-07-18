using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = player.position + offset;
        //gameObject.transform.LookAt(player);
    }
}
