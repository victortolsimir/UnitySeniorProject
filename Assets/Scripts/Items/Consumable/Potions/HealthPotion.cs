using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable/Potion/HealthPotion")]
public class HealthPotion : Potion
{
    public override void Use()
    {
        base.Use();
        GameObject.Find("Player").GetComponent<Character>().GainHealth(restoreValue);
    }

    public override string GetRestores()
    {
        string result = base.GetRestores();
        result += "Restores " + restoreValue + " points of player's Health.\n";
        return result;
    }
}
