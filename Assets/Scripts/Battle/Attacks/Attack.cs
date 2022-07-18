using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public new string name;
    public string description = "";
    public int cost;
    [SerializeField]
    private DebuffEffects debuffEffects;
    [SerializeField]
    private BuffEffects buffEffects;

    public abstract void Activate(BattleCharacter attacker, BattleCharacter defender);

    protected void ApplyDebuffs(BattleCharacter character, int bonusDamage = 0)
    {
        if (debuffEffects is DebuffEffects.None) return;

        var statusEffectManager = character.GetComponent<StatusEffectManager>();
        if (debuffEffects.HasFlag(DebuffEffects.Burn))
        {
            statusEffectManager.ApplyBurn(3, bonusDamage);
        }

        if (debuffEffects.HasFlag(DebuffEffects.Bleed))
        {
            statusEffectManager.ApplyBleed(3, bonusDamage);
        }

        if (debuffEffects.HasFlag(DebuffEffects.Slow))
        {
            statusEffectManager.ApplySlow(3);
        }
    }

    protected void ApplyBuffs(BattleCharacter character, int duration, int buffValue)
    {
        if (buffEffects is BuffEffects.None) return;

        if (buffValue <= 0) return;

        if (duration <= 0) return;

        var statusEffectManager = character.GetComponent<StatusEffectManager>();
        if (buffEffects.HasFlag(BuffEffects.PhysicalResist))
        {
            statusEffectManager.ApplyPhysicalResistBuff(duration, buffValue);
        }

        if (buffEffects.HasFlag(BuffEffects.MagicResist))
        {
            statusEffectManager.ApplyMagicResistBuff(duration, buffValue);
        }
    }

    protected int CalculateCritDamage(int critDamage, int baseDamage)
    {
        int bonusDamage = baseDamage * critDamage / 100;
        return bonusDamage;
    }

    protected bool IsCriticalHit(BattleCharacter character)
    {
        var rand = new System.Random();
        int critChance = character.stats.criticalChance.GetValue();
        int roll = rand.Next(1, 101);
        return critChance >= roll;
    }

    [System.Flags]
    public enum DebuffEffects
    {
        None = 0,
        Burn = 1,
        Bleed = 2,
        Slow = 4
    }

    [System.Flags]
    public enum BuffEffects
    {
        None = 0,
        PhysicalResist = 1,
        MagicResist = 2
    }
}
