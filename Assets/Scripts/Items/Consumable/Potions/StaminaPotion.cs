using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable/Potion/StaminaPotion")]
public class StaminaPotion : Potion
{
    public override void Use()
    {
        base.Use();
        GameObject.Find("Player").GetComponent<Character>().GainStamina(restoreValue);
    }

    public override string GetRestores()
    {
        string result = base.GetRestores();
        result += "Restores " + restoreValue + " points of player's Stamina.\n";
        return result;
    }
}
