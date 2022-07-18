using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectionGoal : Goal
{
    public string itemName;
    public List<Item> itemsToCollect;

    public CollectionGoal(Quest quest, List<Item> itemsToCollect, string description, bool completed, int currentAmount, int requiredAmount, string goalType)
    {
        this.itemsToCollect = itemsToCollect;
        //this.itemName = itemName;
        this.description = description;
        this.completed = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
        this.goalType = goalType;
    }

    public override void Init()
    {
        base.Init();
    }



    public void ItemCollected(Item theItem, InventorySO inventory)
    {

        if(inventory.items["Misc"].Contains(theItem))
        {
            foreach(Item item in inventory.items["Misc"])
            {
                if(itemsToCollect.Contains(item))
                {
                    this.currentAmount++;
                }
            }
            Evaluate();
        }

        /*if (itemsToCollect.Contains(theItem))
            this.currentAmount++;

        Evaluate();*/
    }

    private void CheckInventoryForKeys()
    {
        //var keys = Inventory.instance.items["Misc"];
    }
}
