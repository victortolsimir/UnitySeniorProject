using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceRequirement : MonoBehaviour
{
    [SerializeField]
    private QuestSO requiredQuest;

    private BoxCollider boxCollider;

    private bool hasTriggered = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!hasTriggered)
        {
            if (QuestController.instance.completedQuests.Contains(requiredQuest) || QuestController.instance.currentQuest == requiredQuest)
            {
                hasTriggered = true;
                boxCollider.enabled = true;
            }
        }
    }
}
