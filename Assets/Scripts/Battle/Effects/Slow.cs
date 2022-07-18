
public class Slow : StatusEffect
{
    private float slowPercentage = .25f;
    private int slowAmount = 1;

    public Slow(int duration) : base(duration)
    {

    }

    public override void Apply(BattleCharacter owner)
    {
        CalculateSlowAmount(owner.stats.attackSpeed.GetValue());
        owner.AddAttackSpeedMod(-slowAmount);
    }

    public override void ActivateEffect(BattleCharacter owner)
    {
        if (IsExpired())
        {
            RemoveDebuff(owner);
        }
    }

    private void CalculateSlowAmount(int attackSpeed)
    {
        slowAmount = (int)(attackSpeed * slowPercentage);
    }

    private void RemoveDebuff(BattleCharacter owner)
    {
        owner.RemoveAttackSpeedMod(-slowAmount);
    }

    public override void ClearEffect(BattleCharacter owner)
    {
        RemoveDebuff(owner);
    }
}
