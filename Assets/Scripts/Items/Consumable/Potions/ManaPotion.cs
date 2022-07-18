using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable/Potion/ManaPotion")]
public class ManaPotion : Potion
{
    public override void Use()
    {
        base.Use();
        GameObject.Find("Player").GetComponent<Character>().GainMana(restoreValue);
    }

    public override string GetRestores()
    {
        string result = base.GetRestores();
        result += "Restores " + restoreValue + " points of player's Mana.\n";
        return result;
    }
}
