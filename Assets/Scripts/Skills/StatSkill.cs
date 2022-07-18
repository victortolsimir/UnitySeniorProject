using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : Skill
{
    private int value = 10;

    private string StatName { get; }

    public StatSkill(string statName)
    {
        StatName = statName;
    }

    protected override void ApplyLevelUpEffect()
    {
        var player = GameObject.Find("Player").GetComponent<Character>();

        switch (StatName)
        {
            case "Health":
                player.AddMaxHealthMod(value);
                break;
            case "Mana":
                player.AddMaxManaMod(value);
                break;
            case "Stamina":
                player.AddMaxStaminaMod(value);
                GlobalControl.instance.inventory.weightLimit += value;
                break;
            case "Physical Resist":
                player.AddPhysicalResistMod(value);
                break;
            case "Magic Resist":
                player.AddMagicResistMod(value);
                break;
            default:
                break;
        }
    }
}
