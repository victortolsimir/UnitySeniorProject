using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalResistBuff : StatusEffect
{
    public int Resistance { get; }

    public PhysicalResistBuff(int duration, int resistance) : base(duration + 1)
    {
        Resistance = resistance;
    }

    public override void ActivateEffect(BattleCharacter owner)
    {
        if (IsExpired())
        {
            RemoveDebuff(owner);
        }
    }

    private void RemoveDebuff(BattleCharacter owner)
    {
        owner.RemovePhysicalResistMod(Resistance);
    }

    public override void Apply(BattleCharacter owner)
    {
        owner.AddPhysicalResistMod(Resistance);
    }

    public override void ClearEffect(BattleCharacter owner)
    {
        RemoveDebuff(owner);
    }
}
