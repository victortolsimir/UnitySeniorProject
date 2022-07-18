using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/Consumable/Food")]
public class Food : Consumable
{
    public int restoreValue;

    public override string GetItemType() => "Food";
    public override string UseAdjective() => "Eat";


    public override void Use()
    {
        base.Use();
        GameObject.Find("Player").GetComponent<Character>().GainHealth(restoreValue);
        AmbientAudio.PlayEatSound();
    }

    public override string GetRestores()
    {
        string result = base.GetRestores();
        result += "Restores " + restoreValue + " points of player's Health.\n";
        return result;
    }

}
