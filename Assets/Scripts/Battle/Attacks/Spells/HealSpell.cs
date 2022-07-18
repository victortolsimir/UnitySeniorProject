using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Attack/Spell/HealSpell")]
public class HealSpell : Spell
{
    [SerializeField]
    private int _buffValue = 0;
    public int BuffValue { get => _buffValue; }

    [SerializeField]
    private int _buffDuration = 0;
    public int BuffDuration { get => _buffDuration; }

    public override void Activate(BattleCharacter attacker, BattleCharacter defender)
    {
        int healAmount = BaseValue + (int)(attacker.stats.magicDamage.GetValue() * SpellMultiplier);
        attacker.controller.CastDefensive();
        attacker.GainHealth(healAmount);
        ApplyBuffs(attacker, BuffDuration, BuffValue);
        attacker.onAttackComplete?.Invoke();
    }

    public void ActivateOutOfBattle(Character attacker)
    {
        int healAmount = BaseValue + (int)(attacker.stats.magicDamage.GetValue() * SpellMultiplier);
        attacker.GainHealth(healAmount);
    }
}
