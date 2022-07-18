using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Consumable/Drink")]
public class Drink : Consumable
{
    public int restoreValue;

    public override string GetItemType() => "Food";
    public override string UseAdjective() => "Drink";

    public override void Use()
    {
        base.Use();
        GameObject.Find("Player").GetComponent<Character>().GainHealth(restoreValue);
        AmbientAudio.PlayDrinkSound();
    }

    public override string GetRestores()
    {
        string result = base.GetRestores();
        result += "Restores " + restoreValue + " points of player's Health.\n";
        return result;
    }

}
