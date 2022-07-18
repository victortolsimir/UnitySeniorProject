using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicResistBuff : StatusEffect
{
    public int Resistance { get; }

    public MagicResistBuff(int duration, int resistance) : base(duration + 1)
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
        owner.RemoveMagicResistMod(Resistance);
    }

    public override void Apply(BattleCharacter owner)
    {
        owner.AddMagicResistMod(Resistance);
    }

    public override void ClearEffect(BattleCharacter owner)
    {
        RemoveDebuff(owner);
    }
}
