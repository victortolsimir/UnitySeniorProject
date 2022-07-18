using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclusion : MonoBehaviour
{
    public UniqueID uniqueID;
    public ExclusionList exclusionList;
    public bool inverse = false;

    void Start()
    {
        if (uniqueID != null)
        {
            if (exclusionList.exclusions.Contains(uniqueID))
            {
                gameObject.SetActive(inverse);
            }
            else
            {
                gameObject.SetActive(!inverse);
            }
        }
    }
}
