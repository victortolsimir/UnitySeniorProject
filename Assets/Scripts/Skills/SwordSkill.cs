using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : WeaponSkill
{
    protected override void ApplyLevelUpEffect()
    {
        if (Level > 5)
        {
            var player = GameObject.Find("Player").GetComponent<Character>();
            player.AddPhysicalDamageMod(Value);
        }
    }

}
