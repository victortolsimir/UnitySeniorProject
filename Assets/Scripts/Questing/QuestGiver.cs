using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : Npc
{
    public QuestSO theQuest { get; set; }

    // private bool firstInteraction;
    private float timeToWait;
    [SerializeField]
    private Text questNameBox;

    [SerializeField]
    private GameObject questNameCanvas;

    public GameObject lootBagPrefab;

    public GameObject thePlayer;

    [SerializeField]
    private InventorySO inventory;

    public void Start()
    {
        theQuest = QuestController.instance.currentQuest;
        // firstInteraction = QuestController.instance.firstQuestInteraction;
        timeToWait = 0.5f;
        questNameCanvas.SetActive(false);
    }


    public override void Interact()
    {
        //if (theQuest is null)//QuestController.instance.questIndex >= QuestController.instance.allQuests.Count)
        //{
        //    MessagePanelSystem.ShowMessage("Congrats! You have completed all the quests and returned our precious" +
        //        " castle to us. Thank you so much");
        //}
        //else if (!theQuest.assigned)
        //{

        //    //base.Interact();
        //    if(firstInteraction)
        //    {
        //        GreetAndAssignQuest();
        //    }

        //    StartCoroutine(AssignQuest());
        //}
        //else if (theQuest.assigned && !theQuest.completed)
        //{
        //    QuestInProgress();

        //}
        //else if (theQuest.assigned && theQuest.completed)
        //{
        //    QuestComplete();
        //}
        //else
        //{
        //    MessagePanelSystem.ShowMessage("Shouldnt get here");
        //}

        if (!theQuest)
        {
            // check if all quests done
            if (AllQuestsComplete())
            {
                MessagePanelSystem.ShowMessage("Congrats! You have completed all the quests and returned our precious" +
                " castle to us. Thank you so much");
                QuestTrackerUI.UpdateUI("All Quests Complete!");
            }
            else
            {
                // if not all quests done, grant quest to player
                StartCoroutine(AssignQuest());
            }
        }
        else
        {
            if (theQuest.IsComplete())
            {
                QuestComplete();
            }
            else
            {
                QuestInProgress();
            }
        }
    }

    private bool AllQuestsComplete() => QuestController.instance.completedQuests.Count == QuestController.instance.allQuests.Count;

    public IEnumerator AssignQuest()
    {
        yield return new WaitForSeconds(timeToWait);
        //theQuest.assigned = true;
        theQuest = GetNextQuest();
        QuestController.instance.currentQuest = theQuest;
        MessagePanelSystem.ShowMessage(theQuest.IntroQuestDialogue);
        /*if (theQuest.IsComplete())
            QuestTrackerUI.UpdateUI("Quest Complete. Return to Jakan.");
        else
            QuestTrackerUI.UpdateUI(theQuest.GetQuestDescription());*/
        QuestTrackerUI.UpdateUI("");
        questNameBox.text = theQuest.QuestName;
        questNameCanvas.SetActive(true);

        yield return new WaitForSeconds(3f);

        questNameCanvas.SetActive(false);
        
        
    }

    public void QuestInProgress() => MessagePanelSystem.ShowMessage(theQuest.ActiveQuestDialogue);

    public void QuestComplete()
    {
        // theQuest.assigned = false;
        // MessagePanelSystem.ShowMessage(theQuest.QuestName + " quest is completed! Your reward will be at your feet.");
        MessagePanelSystem.ShowMessage(theQuest.CompletedQuestDialogue);
        QuestTrackerUI.RemoveCurrentQuest();

        // StartCoroutine(QuestRewards());
        if (theQuest.ItemRewards.Count > 0)
        {
            var lootBag = Instantiate(lootBagPrefab, thePlayer.transform.position, Quaternion.identity).GetComponent<LootBag>();
            lootBag.Items = theQuest.ItemRewards;
        }
        inventory.gold += theQuest.GoldReward;
        if (theQuest.SpecialItem != null)
            Inventory.instance.AddItem(theQuest.SpecialItem);
        QuestController.instance.completedQuests.Add(theQuest);
        theQuest = null;
        QuestController.instance.currentQuest = null;
        //theQuest.GiveReward(lootBag);
        // NextQuest();
    }

    //private void NextQuest()
    //{
    //    QuestController.instance.curQuestIndex++;
    //    if (QuestController.instance.curQuestIndex < QuestController.instance.allQuests.Count)
    //    {
    //        QuestController.instance.currentQuest = QuestController.instance.allQuests[QuestController.instance.curQuestIndex];
    //        theQuest = QuestController.instance.currentQuest;
    //    }
    //    else
    //    {
    //        theQuest = null;
    //        QuestController.instance.currentQuest = null;
    //    }



    //}

    private QuestSO GetNextQuest()
    {
        return QuestController.instance.GetNextQuest();
    }

    //private void GreetAndAssignQuest()
    //{

    //    MessagePanelSystem.ShowMessage("Hi my name is Jakan");
    //    this.firstInteraction = false;
    //    QuestController.instance.firstQuestInteraction = false;
    //}

    //private IEnumerator QuestRewards()
    //{
    //    yield return new WaitForSeconds(1f);
    //    string rewards = "You recieved ";

    //    foreach (Item item in theQuest.itemRewards)
    //    {
    //        rewards += item.name + ", ";
    //    }

    //    if (theQuest.specialItemReward != null)
    //        rewards += theQuest.specialItemReward.name + ", ";

    //    rewards += theQuest.goldReward;
    //}
}
