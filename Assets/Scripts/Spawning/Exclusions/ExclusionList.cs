using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Exclusions/ExclusionList")]
public class ExclusionList : ScriptableObject
{
    public List<UniqueID> exclusions;

    public void Clear()
    {
        exclusions.Clear();
    }

    public void Add(UniqueID uniqueID)
    {
        exclusions.Add(uniqueID);
    }
}
