using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public enum EnemyType {None, Beetle, GiantChicken, BossGolem, Bat, SpecialMagma, BossTreant, Cobra, Treant, Wolf, Spider, Chicken, BossCobra, Dragon, BossDragon, BossWolf}

    #region old
    /*
    [SerializeField]
    public List<Quest> allQuests;


    public static QuestController instance;
    public int curQuestIndex;
    public int currentAmount;
    public bool firstQuestInteraction;
    [SerializeField]
    public ExclusionList bossExclusion;
    [SerializeField]
    public Quest currentQuest;
    [SerializeField]
    private InventorySO inventory;

    
    private int golemQuestIndex;
    private int giantChickenQuestIndex;
    private int treantQuestIndex;
    private int specialCobraQuestIndex;
    private int cobraQuestIndex;

    public void Awake()
    {
        if (instance is null)
        {
            DontDestroyOnLoad(gameObject);
            this.allQuests = new List<Quest>();
            CreateQuests();
            currentQuest = allQuests[0];
            this.firstQuestInteraction = true;
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void ManualDestroy()
    {
        if (instance)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    public void EnemyWasKilled(EnemyType enemyID, UniqueID uniquePrefab)
    {
        if (currentQuest.assigned)
        {
            List<Goal> goalList = currentQuest.goals;

            foreach (Goal g in goalList)
            {
                if (g.goalType.CompareTo("kill") == 0)
                {
                    ((KillGoal)g).EnemyDied(enemyID);

                }
            }

            currentQuest.CheckGoals();
        }

        CheckID(enemyID, uniquePrefab);


    }

    public void ItemWasCollected(Item item)
    {
        if (currentQuest.assigned)
        {
            List<Goal> goalList = currentQuest.goals;
            foreach (Goal g in goalList)
            {
                if (g.goalType.CompareTo("collect") == 0)
                    ((CollectionGoal)g).ItemCollected(item, inventory);
            }

            currentQuest.CheckGoals();
        }

    }

    private void CreateQuests()
    {
        this.allQuests.Add(new StartingOff());

        this.allQuests.Add(new KillBeetles());

        this.allQuests.Add(new ExploreFloor1());
        this.giantChickenQuestIndex = 2;

        this.allQuests.Add(new KillGolem());
        this.golemQuestIndex = 3;

        this.allQuests.Add(new KillBats());

        this.allQuests.Add(new KillTheCobra());
        this.specialCobraQuestIndex = 5;

        this.allQuests.Add(new KillTheTreant());
        this.treantQuestIndex = 6;

        this.allQuests.Add(new Kill3rdFloorMonsters());

        this.allQuests.Add(new KillKingCobra());
        this.cobraQuestIndex = 8;


    }

    private void CheckID(EnemyType id, UniqueID prefabID)
    {
        Quest quest = null;

        if(id == EnemyType.BossGolem)
        {
            quest = this.allQuests[golemQuestIndex];
        }
        else if(id == EnemyType.GiantChicken)
        {
            quest = this.allQuests[giantChickenQuestIndex];
        }
        else if(id == EnemyType.SpecialCobra)
        {
            quest = this.allQuests[specialCobraQuestIndex];
        }
        else if(id == EnemyType.BossTreant)
        {
            quest = this.allQuests[treantQuestIndex];
        }
        else if(id == EnemyType.BossCobra)
        {
            quest = this.allQuests[cobraQuestIndex];
        }


        if (quest != null)
        {
            foreach(Goal g in quest.goals)
            {
                ((UniqueKillGoal)g).UniqueEnemyDied(id, prefabID);
            }
            quest.CheckGoals();
        }

    }


    private void ForLater()
    {

    }
    */
    #endregion

    public static QuestController instance;

    public List<QuestSO> allQuests;

    public List<QuestSO> completedQuests;

    public QuestSO currentQuest;

    public string questTrackerInfo;

    //[SerializeField]
    //private InventorySO inventory;

    public void Awake()
    {
        if (instance is null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            ResetAllQuests();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // ResetAllQuests();
    }

    private void ResetAllQuests()
    {
        allQuests.ForEach(quest => { quest.Reset(); });
    }

    public static void ManualDestroy()
    {
        if (instance)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    public QuestSO GetNextQuest()
    {
        if (completedQuests.Count == 0)
        {
            foreach (var quest in allQuests)
            {
                if (!quest.PrerequisiteQuest)
                    return quest;
            }
        }
        foreach (var quest in allQuests)
        {
            if (!completedQuests.Contains(quest) && completedQuests.Contains(quest.PrerequisiteQuest))
            {
                return quest;
            }
        }
        return null;
    }

    public void EnemyWasKilled(EnemyType enemyID)
    {
        if (currentQuest)
        {
            currentQuest.EnemyKilled(enemyID);
        }
    }

    public List<int> SaveGoalProgress()
    {
        var list = new List<int>();
        if (currentQuest is KillQuestSO killQuest)
        {
            list = killQuest.GetGoalProgress();
        }

        return list;
    }

    public void LoadGoalProgress(List<int> goalProgress)
    {
        if (currentQuest is KillQuestSO killQuest)
        {
            killQuest.SetGoalProgress(goalProgress);
        }
    }
}
