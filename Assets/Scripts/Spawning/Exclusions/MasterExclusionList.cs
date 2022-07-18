using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Exclusions/MasterExclusionList")]
public class MasterExclusionList : ScriptableObject
{
    [SerializeField]
    private List<ExclusionList> exclusionLists;

    public void ClearAll()
    {
        foreach (var exclusionList in exclusionLists)
        {
            exclusionList.Clear();
        }
    }
}
