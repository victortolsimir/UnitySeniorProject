
public class Burn : StatusEffect
{
    public int Damage { get; }
    private int baseDamage = 5;

    public Burn(int duration, int bonusDamage) : base(duration)
    {
        Damage = baseDamage + bonusDamage;
    }

    public override void Apply(BattleCharacter owner) { }

    public override void ActivateEffect(BattleCharacter owner)
    {
        owner.TakeDamage(new Damage(0, Damage, 0, 0));
    }
}
