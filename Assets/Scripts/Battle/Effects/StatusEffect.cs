using System.Collections;
using System.Collections.Generic;

public abstract class StatusEffect
{
    public int Duration { get; private set; }

    public StatusEffect(int duration)
    {
        Duration = duration;
    }

    public abstract void Apply(BattleCharacter owner);

    public void Activate(BattleCharacter owner)
    {
        Duration--;
        ActivateEffect(owner);
    }

    public abstract void ActivateEffect(BattleCharacter owner);

    public void IncreaseDuration(int value)
    {
        Duration += value;
    }

    public bool IsExpired()
    {
        return Duration <= 0;
    }

    public virtual void ClearEffect(BattleCharacter owner) { }
}
