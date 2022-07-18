using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTest : MonoBehaviour
{
    public SkinnedMeshRenderer item;
    public SkinnedMeshRenderer targetMesh;

    /*public GameObject characterDisplay;
    private BoneCombiner boneCombiner;

    // Start is called before the first frame update
    void Start()
    {
        //uses the gameObject that this script is attatched to which should be the player object
        boneCombiner = new BoneCombiner(gameObject); 
        EquipItem(characterDisplay);
    }

    private void EquipItem(GameObject characterDisplay)
    {
        boneCombiner.AddLimb(characterDisplay);
    }*/

    private void Start()
    {
        EquipItem(item);
    }

    private void EquipItem(SkinnedMeshRenderer item)
    {
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(item);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
    }
}
