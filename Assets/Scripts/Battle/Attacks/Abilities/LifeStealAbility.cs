using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Attack/Ability/LifeStealAbility")]
public class LifeStealAbility : DamageAbility
{
    [SerializeField]
    private int healAmount;

    public override void Activate(BattleCharacter attacker, BattleCharacter defender)
    {
        base.Activate(attacker, defender);
        attacker.GainHealth(healAmount);
    }
}
