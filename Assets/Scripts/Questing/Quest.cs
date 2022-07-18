using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

[System.Serializable]

public class Quest 
{
    
    [SerializeField]
    public List<Goal> goals  = new List<Goal>();

    public string questName;
    public string description;
    public Item specialItemReward;
    public int goldReward;
    public bool completed;
    public List<Item> itemRewards;

    public bool assigned;


    public bool CheckGoals()
    {
        
        foreach(Goal g in goals)
        {

            if (!g.completed)
                return false;
        }

        completed = true;

        return true;
    }

    public void Complete()
    {
        completed = true;
    }

    //Give item reward and adds them to inventory
    public virtual void GiveReward(LootBag lootBag)
    {
        /*if (itemReward != null)
        {
            Inventory.instance.AddItem(this.itemReward);
        }
        */
        if(this.specialItemReward != null)
        {
            Inventory.instance.AddItem(this.specialItemReward);
        }

        Inventory.instance.inventorySO.gold += this.goldReward;

        if(itemRewards.Count != 0)
        {
            lootBag.Items = this.itemRewards;
        }
    }

 
}
