using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : WeaponSkill
{
    protected override void ApplyLevelUpEffect()
    {
        if (Level > 3)
        {
            var player = GameObject.Find("Player").GetComponent<Character>();
            player.AddMaxHealthMod(Value * 2);
            player.AddPhysicalResistMod(Value * 2);
            player.AddMagicResistMod(Value * 2);
        }
    }
}
