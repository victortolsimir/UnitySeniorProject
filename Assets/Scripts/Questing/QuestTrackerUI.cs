using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerUI : MonoBehaviour
{
    #region old
    /*
    [SerializeField]
    private Text questBox;
    private Quest quest;


    // Update is called once per frame
    void Update()
    {
        quest = QuestController.instance.currentQuest;
        string descriptions = "";
        if (QuestController.instance.curQuestIndex >= QuestController.instance.allQuests.Count)
            questBox.text = "All quest complete";

        else if (quest.completed)
            questBox.text = "Quest complete! Please talk to Jakan.";

        else if (quest.assigned)
        {
            List<Goal> goals = quest.goals;
            foreach (Goal g in goals)
            {
                descriptions += g.description + " " + g.currentAmount + "/" + g.requiredAmount + "\n";
            }

            questBox.text = descriptions;
        }
       
    }

    private void Awake()
    {
        questBox.text = "";
    }
    */
    #endregion

    private static QuestTrackerUI instance;

    [SerializeField]
    private Transform questInfo;
    [SerializeField]
    private Transform Message;

    private QuestSO quest;

    private string info;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        //info = QuestController.instance.questTrackerInfo;
        OnUpdateUI(info);
    }

    public static void UpdateUI(string info)
    {
        if (instance)
            instance.OnUpdateUI(info);
    }

    private void OnUpdateUI(string info)
    {
        this.quest = QuestController.instance.currentQuest;
        
        if (AllQuestsComplete())
        {
            ShowMessageOnly();
            Text message = Message.GetComponent<Text>();
            message.text = "";
            Text tip = Message.Find("Tip").GetComponent<Text>();
            tip.text = "All Quests Complete!";
            return;

        }

        if(quest == null)
        {
            ShowMessageOnly();
            return;
        }
       
        questInfo.Find("Name").GetComponent<Text>().text = quest.QuestName;
        questInfo.Find("Description").GetComponent<Text>().text = quest.Description;
        questInfo.Find("Tasks").GetComponent<Text>().text = GetTasks();

        Text rewards = questInfo.Find("RewardsContent").Find("Rewards").GetComponent<Text>();
        rewards.text = "";
        if (quest.GoldReward != 0)
        {
            rewards.text += $"Gold Reward: {quest.GoldReward}gp\n";
        }
        foreach (Item reward in quest.ItemRewards)
        {
            rewards.text += $"{reward.name}\n";
        }

        ShowQuestInfoOnly();

        
        /*this.info = info;
        QuestController.instance.questTrackerInfo = info;
        questBox.text = info;*/
        

    }

    private void ShowMessageOnly()
    {
        Text message = Message.GetComponent<Text>();
        message.text = "No Current Quests.";
        Text tip = Message.Find("Tip").GetComponent<Text>();
        tip.text = "Go see Jakan in the town. He stands under a tree by the bridge.";
        questInfo.gameObject.SetActive(false);
        Message.gameObject.SetActive(true);
    }

    private void ShowQuestInfoOnly()
    {
        questInfo.gameObject.SetActive(true);
        Message.gameObject.SetActive(false);
    }

    private string GetTasks()
    {
        string completedQuest = "Quest Complete. Return to Jakan.";
        string tasks = "";
        if(quest is UniqueKillQuestSO)
        {
            UniqueKillQuestSO uniqueKillQuest = (UniqueKillQuestSO)quest;
            if (uniqueKillQuest.goal.IsComplete())
            {
                tasks = $"<color=lime>{completedQuest}</color>";
                return tasks;
            }
            tasks = $"<color=red>{uniqueKillQuest.goal.Description}</color>";

        }
        else if(quest is KillQuestSO)
        {
            KillQuestSO killQuest = (KillQuestSO)quest;
            if (killQuest.IsComplete())
            {
                tasks = $"<color=lime>{completedQuest}</color>";
                return tasks;
            }
            List<TestKillGoal> goals = killQuest.goals;
            foreach(TestKillGoal goal in goals)
            {
                string goalDescription = goal.Description;
                if (goal.IsComplete()) { tasks += $"<color=lime>{goalDescription}</color>\n"; }
                else { tasks += $"<color=red>{goalDescription}</color>\n"; }
            }
        }
        return tasks;
    }

    public static void RemoveCurrentQuest()
    {
        if (instance)
            instance.ShowMessageOnly();
    }


    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void OpenQuestTrackerUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private bool AllQuestsComplete() => QuestController.instance.completedQuests.Count == QuestController.instance.allQuests.Count;
}
