using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPortalMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject firstFloorButton;

    [SerializeField]
    private QuestSO requiredQuest;

    [SerializeField]
    private List<GameObject> floorButtons;

    [SerializeField]
    private List<UniqueID> floorRequirements;

    [Space(10)]
    [SerializeField]
    private ExclusionList bossList;

    [SerializeField]
    private UniqueID spawnID;

    private void Start()
    {
        if (bossList)
        {
            for (int i = 0; i < floorButtons.Count; i++)
            {
                EnableButtons(floorButtons[i], floorRequirements[i]);
            }
        }
        else
        {
            firstFloorButton.SetActive(true);
            floorButtons.ForEach(button => { button.SetActive(true); });
        }
    }

    private void OnEnable()
    {
        if (!firstFloorButton.activeSelf)
        {
            if (QuestController.instance.completedQuests.Contains(requiredQuest) || QuestController.instance.currentQuest == requiredQuest)
            {
                firstFloorButton.SetActive(true);
            }
        }
    }

    private void EnableButtons(GameObject button, UniqueID requirement)
    {
        if (bossList.exclusions.Contains(requirement)) button.SetActive(true);
    }

    public void TeleportToDungeon(string dungeonFloor)
    {
        // GlobalControl.instance.IsTeleporting = true;
        GlobalControl.instance.spawnID = spawnID;
        SceneLoader.Load(dungeonFloor);
    }
    public void OnExitButton()
    {
        GameObject.Find("Player").GetComponent<MyCharacterController>().EnableMovement();
        gameObject.SetActive(false);
    }
}
