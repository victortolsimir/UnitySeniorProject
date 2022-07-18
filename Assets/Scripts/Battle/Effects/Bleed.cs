using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffect
{
    public int Damage { get; }
    private int baseDamage = 5;

    public Bleed(int duration, int bonusDamage) : base(duration)
    {
        Damage = baseDamage + bonusDamage;
    }

    public override void Apply(BattleCharacter owner) { }

    public override void ActivateEffect(BattleCharacter owner)
    {
        owner.TakeDamage(new Damage(Damage, 0, 0, 0));
    }
}
