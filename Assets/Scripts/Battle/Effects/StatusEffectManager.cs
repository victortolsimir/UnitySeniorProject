using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleCharacter))]
public class StatusEffectManager : MonoBehaviour
{
    private BattleCharacter character;
    private List<StatusEffect> effects = new List<StatusEffect>();

    private void Start()
    {
        character = GetComponent<BattleCharacter>();
    }

    public void ApplyBurn(int duration, int bonusDamage)
    {
        AddEffect(new Burn(duration, bonusDamage));
    }

    public void ApplyBleed(int duration, int bonusDamage)
    {
        AddEffect(new Bleed(duration, bonusDamage));
    }

    public void ApplySlow(int duration)
    {
        bool slowExists = false;
        foreach (var effect in effects)
        {
            if (effect is Slow)
            {
                slowExists = true;
                effect.IncreaseDuration(duration);
                break;
            }
        }
        if (!slowExists)
        {
            AddEffect(new Slow(duration));
        }
    }

    public void ApplyPhysicalResistBuff(int duration, int value)
    {
        bool buffExists = false;
        foreach (var effect in effects)
        {
            if (effect is PhysicalResistBuff resistBuff)
            {
                if (resistBuff.Resistance == value)
                {
                    buffExists = true;
                    effect.IncreaseDuration(duration);
                    break;
                }
            }
        }
        if (!buffExists)
        {
            AddEffect(new PhysicalResistBuff(duration, value));
        }
    }

    public void ApplyMagicResistBuff(int duration, int value)
    {
        bool buffExists = false;
        foreach (var effect in effects)
        {
            if (effect is MagicResistBuff resistBuff)
            {
                if (resistBuff.Resistance == value)
                {
                    buffExists = true;
                    effect.IncreaseDuration(duration);
                    break;
                }
            }
        }
        if (!buffExists)
        {
            AddEffect(new MagicResistBuff(duration, value));
        }
    }

    private void AddEffect(StatusEffect effect)
    {
        effects.Add(effect);
        effect.Apply(character);
    }

    public void HandleEffects()
    {
        foreach (StatusEffect effect in effects)
        {
            effect.Activate(character);
        }
        effects.RemoveAll(effect => effect.IsExpired());
    }

    public void ClearEffects()
    {
        foreach (StatusEffect effect in effects)
        {
            effect.ClearEffect(character);
        }

        effects.Clear();
    }
}
