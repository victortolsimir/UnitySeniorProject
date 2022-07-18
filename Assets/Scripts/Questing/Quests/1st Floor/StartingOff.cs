using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingOff : Quest
{
    public StartingOff()
    {
        base.questName = "Easy Peasy";
        base.description = "Kill the tiny chicken";
        //base.itemReward = (Item)Resources.Load("Items/Misc/Keys/Key1-2-1");
        base.itemRewards = new List<Item>();
        //itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Chest/Leather PlateBody"));

        FillItemRewards();

        QuestController.EnemyType enemyID = QuestController.EnemyType.Chicken;
        int curAmount = 0;
        int requireAmount = 1;

        base.goals.Add(new KillGoal(enemyID, description, false, curAmount, requireAmount, "kill"));

        foreach (Goal g in this.goals)
        {
            g.Init();
        }

    }

    public override void GiveReward(LootBag lootBag)
    {
        base.GiveReward(lootBag);
    }

    private void FillItemRewards()
    {
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Chest/Leather PlateBody"));
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Arms/Leather Armor Gloves"));
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Feet/Leather Boots"));
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Helmets/Leather Cap"));
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Armor/Legs/Leather Leggings"));
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Weapons/Swords/Basic Iron Sword"));
        itemRewards.Add((Item)Resources.Load("Items/Equippable/Weapons/Shields/Basic WoodenShield"));
    }
}
