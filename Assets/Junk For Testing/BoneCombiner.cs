using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCombiner
{
    public readonly Dictionary<int, Transform> rootBoneDictionary = new Dictionary<int, Transform>();
    private readonly Transform[] boneTransforms = new Transform[63];

    private readonly Transform _transform; //the player object
    public BoneCombiner(GameObject rootObj)
    {
        _transform = rootObj.transform;
        TraverseHierarchy(_transform);
    }

    

    //boneObj is the gameobject we want to equip
    public Transform AddLimb(GameObject boneObj)
    {
        Transform limb = ProcessBonedObject(boneObj.GetComponentInChildren<SkinnedMeshRenderer>());
        limb.SetParent(_transform);
        return limb;
    }

    private Transform ProcessBonedObject(SkinnedMeshRenderer renderer)
    {
        var bonedObject = new GameObject().transform;
        var meshRenderer = bonedObject.gameObject.AddComponent<SkinnedMeshRenderer>();
        var bones = renderer.bones;

        for(int i = 0; i < bones.Length; i++)
        {
            boneTransforms[i] = rootBoneDictionary[bones[i].name.GetHashCode()];
        }

        meshRenderer.bones = boneTransforms;
        meshRenderer.sharedMesh = renderer.sharedMesh;
        meshRenderer.materials = renderer.materials;

        return bonedObject;
    }

    private void TraverseHierarchy(Transform transform)
    {
        foreach(Transform child in transform)
        {
            rootBoneDictionary.Add(child.name.GetHashCode(), child);
            TraverseHierarchy(child);
        }
    }
}
