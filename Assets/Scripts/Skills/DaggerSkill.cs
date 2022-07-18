using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerSkill : WeaponSkill
{
    protected override void ApplyLevelUpEffect()
    {
        if (Level > 3)
        {
            var player = GameObject.Find("Player").GetComponent<Character>();
            player.AddPhysicalDamageMod(Value / 2);
            player.AddCriticalChanceMod(Value / 2);
            player.AddCriticalDamageMod(Value);
        }
    }
}
