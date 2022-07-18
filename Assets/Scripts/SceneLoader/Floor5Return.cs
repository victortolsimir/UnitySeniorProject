using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor5Return : MonoBehaviour
{
    [SerializeField]
    public String sceneName = "Dungeon_Floor_5";
    [SerializeField]
    private UniqueID spawnID;

    private void OnTriggerEnter()
    {
        GoToFloor5();
    }

    private void GoToFloor5()
    {
        // GlobalControl.instance.IsTeleporting = true;
        GlobalControl.instance.spawnID = spawnID;
        SceneLoader.Load(sceneName);
    }
}
