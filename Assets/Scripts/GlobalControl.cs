using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class GlobalControl : MonoBehaviour
{

    public enum PlayerState { Standard, EnteringBattle, ReturningFromBattle, Loading }

    public static GlobalControl instance;
    public QuestController testQuestController = QuestController.instance;
    public string SavePath { get; private set; } = "/saves/";

    [Header("Character Details")]
    public string playerName;
    public bool isMale = true;
    public CharacterStats stats;
    public List<Ability> abilities;
    public List<Spell> spells;
    [Space(20)]
    public CharacterSkills characterSkills;
    [Space(20)]
    public Vector3 playerPosition;

    public PlayerState playerstate;
    public string sceneBeforeBattle;
    public BattleInfo battleInfo;

    #region Exclusion Lists
    [Header("Exclusion Lists")]

    [SerializeField]
    private ExclusionList bossExclusions;

    [SerializeField]
    private ExclusionList enemyExclusions;

    [SerializeField]
    private ExclusionList itemExclusions;

    [SerializeField]
    private ExclusionList keyExclusions;

    [SerializeField]
    private ExclusionList doorAndKeyExclusions;

    [Space(10)]
    #endregion

    public InventorySO inventory;

    public EquipmentSO equipment;

    public bool movingToPrevFloor = false;
    public bool cameFromNextFloor = false;
    public UniqueID spawnID;
    //public List<int> currentGoalAmountList;
    //public List<bool> goalCompletionStatus;
    //public List<bool> questCompletionStatus;
    //public List<bool> questAssignmentStatus;
   

    public int enemyId { get; set; }

    private void Awake()
    {
        if (instance is null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            ClearState();
            SavePath = Application.persistentDataPath + SavePath;
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            //currentGoalAmountList = new List<int>();
            //goalCompletionStatus = new List<bool>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void ClearState()
    {
        ClearExclusions();
        instance.inventory.ClearAll();
        instance.battleInfo.wonBattle = false;
        instance.equipment.ClearAll();
    }

    private void ClearExclusions()
    {
        bossExclusions.Clear();
        enemyExclusions.Clear();
        itemExclusions.Clear();
        keyExclusions.Clear();
        doorAndKeyExclusions.Clear();
    }

    public static void ManualDestroy()
    {
        if (instance)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    public void Save()
    {
        Debug.Log("Saving Game...");

        StartCoroutine(Message_SavingGame());

        //PullQuestInfo();

        var toSave = new GameMemento
        {
            weapons = instance.inventory.items["Weapon"],
            food = instance.inventory.items["Food"],
            armor = instance.inventory.items["Armor"],
            potions = instance.inventory.items["Potion"],
            misc = instance.inventory.items["Misc"],
            playerName = instance.playerName,
            isMale = instance.isMale,
            stats = instance.stats,
            abilities = instance.abilities,
            spells = instance.spells,
            skills = instance.characterSkills,
            weightLimit = instance.inventory.weightLimit,
            totalWeight = instance.inventory.totalWeight,
            gold = instance.inventory.gold,
            position = playerPosition,
            sceneToLoad = SceneManager.GetActiveScene().name,

            #region UniqueID saving

            bossExclusions = instance.bossExclusions.exclusions,
            enemyExclusions = instance.enemyExclusions.exclusions,
            itemExclusions = instance.itemExclusions.exclusions,
            keyExclusions = instance.keyExclusions.exclusions,
            doorAndKeyExclusions = instance.doorAndKeyExclusions.exclusions,

            #endregion

            currentEquipment = instance.equipment.currentEquipment,
            defaultEquipment = instance.equipment.defaultEquipment,
            defaultColorPref = instance.equipment.defaultColorPref,

            //Quest saving stuff

            //NPCFirstInteraction = QuestController.instance.firstQuestInteraction,
            //curQuestIndex = QuestController.instance.curQuestIndex,
            //goalCurAmount = instance.currentGoalAmountList,
            //goalCompletionStatus = instance.goalCompletionStatus,
            //questAssigmentStatus = instance.questAssignmentStatus,
            //questCompletionStatus = instance.questCompletionStatus
            currentQuest = QuestController.instance.currentQuest,
            goalCurAmount = QuestController.instance.SaveGoalProgress(),
            completedQuests = QuestController.instance.completedQuests,
            questTrackerInfo = QuestController.instance.questTrackerInfo
            
        };

        string saveString = JsonUtility.ToJson(toSave);
        #if UNITY_EDITOR
        saveString = EditorJsonUtility.ToJson(toSave);
        #endif
        File.WriteAllText($"{SavePath}{playerName}.txt", saveString);

        Debug.Log("Game Saved");
    }

    public void Load(string name)
    {
        string characterSavePath = $"{SavePath}{name}.txt";
        if (!File.Exists(characterSavePath))
        {
            return;
        }

        

        string saveString = File.ReadAllText(characterSavePath);
        var gameMemento = JsonUtility.FromJson<GameMemento>(saveString);
        #if UNITY_EDITOR
        EditorJsonUtility.FromJsonOverwrite(saveString, gameMemento);
        #endif
        #region Inventory loading
        instance.inventory.items["Weapon"] = gameMemento.weapons;
        instance.inventory.items["Food"] = gameMemento.food;
        instance.inventory.items["Armor"] = gameMemento.armor;
        instance.inventory.items["Potion"] = gameMemento.potions;
        instance.inventory.items["Misc"] = gameMemento.misc;
        instance.playerName = gameMemento.playerName;
        instance.isMale = gameMemento.isMale;
        instance.stats = gameMemento.stats;
        instance.abilities = gameMemento.abilities;
        instance.spells = gameMemento.spells;
        instance.characterSkills = gameMemento.skills;
        instance.inventory.weightLimit = gameMemento.weightLimit;
        instance.inventory.totalWeight = gameMemento.totalWeight;
        instance.inventory.gold = gameMemento.gold;
        #endregion
        playerPosition = gameMemento.position;
        playerstate = PlayerState.Loading;

        #region UniqueID loading

        instance.bossExclusions.exclusions = gameMemento.bossExclusions;
        instance.enemyExclusions.exclusions = gameMemento.enemyExclusions;
        instance.itemExclusions.exclusions = gameMemento.itemExclusions;
        instance.keyExclusions.exclusions = gameMemento.keyExclusions;
        instance.doorAndKeyExclusions.exclusions = gameMemento.doorAndKeyExclusions;

        #endregion

        instance.equipment.currentEquipment = gameMemento.currentEquipment;
        instance.equipment.defaultEquipment = gameMemento.defaultEquipment;
        instance.equipment.defaultColorPref = gameMemento.defaultColorPref;
        instance.equipment.SetBlockColors();

        #region Quest loading
        //QuestController.instance.firstQuestInteraction = gameMemento.NPCFirstInteraction;
        //QuestController.instance.curQuestIndex = gameMemento.curQuestIndex;
        //QuestController.instance.currentQuest = QuestController.instance.allQuests[gameMemento.curQuestIndex];
        //instance.currentGoalAmountList = gameMemento.goalCurAmount;
        //instance.goalCompletionStatus = gameMemento.goalCompletionStatus;
        //instance.questCompletionStatus = gameMemento.questCompletionStatus;
        //instance.questAssignmentStatus = gameMemento.questAssigmentStatus;

        //RelayQuestInfo();

        QuestController.instance.currentQuest = gameMemento.currentQuest;
        QuestController.instance.completedQuests = gameMemento.completedQuests;
        QuestController.instance.LoadGoalProgress(gameMemento.goalCurAmount);
        QuestController.instance.questTrackerInfo = gameMemento.questTrackerInfo;

        #endregion



        Debug.Log("Load successful!");

        SceneLoader.Load(gameMemento.sceneToLoad);
    }

    private IEnumerator Message_SavingGame()
    {
        GameObject popUpMessage = GameObject.Find("Hub").transform.Find("PopUpMessage").gameObject;
        Text message = popUpMessage.GetComponentInChildren<Text>();
        popUpMessage.SetActive(true);
        message.text = "Saving Game...";
        yield return new WaitForSeconds(1);
        message.text = "Game Saved";
        yield return new WaitForSeconds(0.5f);
        popUpMessage.SetActive(false);
        ScreenShotHandler.instance.TakeGameSaveShot();
    }

    //private void PullQuestInfo()
    //{
    //    foreach(Quest quest in QuestController.instance.allQuests)
    //    {
    //        questAssignmentStatus.Add(quest.assigned);
    //        questCompletionStatus.Add(quest.completed);

    //        foreach(Goal goal in quest.goals)
    //        {
    //            currentGoalAmountList.Add(goal.currentAmount);
    //            goalCompletionStatus.Add(goal.completed);
    //        }
    //    }
    //}

    //private void RelayQuestInfo()
    //{
    //    int goalIndex = 0;
    //    int questIndex = 0;

    //    foreach(Quest quest in QuestController.instance.allQuests)
    //    {
    //        quest.assigned = instance.questAssignmentStatus[questIndex];
    //        quest.completed = instance.questCompletionStatus[questIndex];

    //        for(int x = 0; x < quest.goals.Count; x++)
    //        {
    //            quest.goals[x].completed = instance.goalCompletionStatus[goalIndex];
    //            quest.goals[x].currentAmount = instance.currentGoalAmountList[goalIndex];
    //            goalIndex++;
    //        }

    //        questIndex++;
    //    }

    //    QuestController.instance.currentQuest.assigned = true;
    //}
}

[Serializable]
class GameMemento
{
    public List<Item> weapons;
    public List<Item> food;
    public List<Item> armor;
    public List<Item> potions;
    public List<Item> misc;

    public string playerName;
    public bool isMale;
    public CharacterStats stats;
    public List<Ability> abilities;
    public List<Spell> spells;
    public CharacterSkills skills;
    public double weightLimit;
    public double totalWeight;
    public int gold;
    public Vector3 position;
    public string sceneToLoad;

    //public List<List<UniqueID>> exclusionLists;
    public List<UniqueID> bossExclusions;
    public List<UniqueID> enemyExclusions;
    public List<UniqueID> itemExclusions;
    public List<UniqueID> keyExclusions;
    public List<UniqueID> doorAndKeyExclusions;


    public Equippable[] currentEquipment;
    public DefaultItem[] defaultEquipment;
    public Color[] defaultColorPref;

    //quest stuff to save
    //public bool NPCFirstInteraction;
    //public int curQuestIndex;
    public QuestSO currentQuest;
    public List<int> goalCurAmount;
    public List<QuestSO> completedQuests;
    public string questTrackerInfo;
    //public List<bool> goalCompletionStatus;
    //public List<bool> questCompletionStatus;
    //public List<bool> questAssigmentStatus;
}
