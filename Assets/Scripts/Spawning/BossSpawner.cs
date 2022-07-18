using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : Spawner
{
    [SerializeField]
    private List<GameObject> doors = new List<GameObject>();

    [SerializeField]
    private string _bossSceneName;
    public string BossSceneName { get => _bossSceneName; }

    private void Start()
    {
        if (!exclusionList.exclusions.Contains(uniqueID))
        {
            Instantiate(prefab, transform);
        }
        else if (doors.Count > 0)
        {
            foreach (var door in doors)
            {
                door.SetActive(false);
            }
        }
    }
}
