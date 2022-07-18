using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Attack/Ability/BuffAbility")]
public class BuffAbility : Ability
{
    [SerializeField]
    private int _buffValue = 0;
    public int BuffValue { get => _buffValue; }

    [SerializeField]
    private int _buffDuration = 0;
    public int BuffDuration { get => _buffDuration; }

    public override void Activate(BattleCharacter attacker, BattleCharacter defender)
    {
        attacker.controller.CastDefensive();
        ApplyBuffs(attacker, BuffDuration, BuffValue);
        attacker.onAttackComplete?.Invoke();
    }
}
