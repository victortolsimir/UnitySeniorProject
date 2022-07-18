using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffSkill : WeaponSkill
{
    protected override void ApplyLevelUpEffect()
    {
        if (Level > 3)
        {
            var player = GameObject.Find("Player").GetComponent<Character>();
            player.AddMagicDamageMod(Value);
        }
    }
}
