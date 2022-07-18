using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Attack/Ability/DamageAbility")]
public class DamageAbility : Ability
{
    public float damageMultiplier;

    [Range(0, 1)]
    public float physicalPen;
    [Range(0, 1)]
    public float magicPen;

    public override void Activate(BattleCharacter attacker, BattleCharacter defender)
    {
        int physicalDamage = (int)(attacker.stats.physicalDamage.GetValue() * damageMultiplier);
        int magicDamage = (int)(attacker.stats.magicDamage.GetValue() * damageMultiplier / 4);

        if (IsCriticalHit(attacker))
        {
            int critDamage = attacker.stats.criticalDamage.GetValue();
            physicalDamage += CalculateCritDamage(critDamage, physicalDamage);
            magicDamage += CalculateCritDamage(critDamage, magicDamage);
            attacker.ShowFloatingText("CRIT");
        }

        Vector3 startingPosition = attacker.GetPosition();
        Vector3 targetPosition = defender.GetPosition() + (startingPosition - defender.GetPosition()).normalized * 2.5f;
        // move to enemy
        attacker.controller.StartAttack();
        attacker.MoveToPosition(targetPosition, () =>
        {
            // deal damage
            attacker.controller.Attack();
            defender.TakeDamage(new Damage(physicalDamage, magicDamage, physicalPen, magicPen));
            ApplyDebuffs(defender, attacker.stats.physicalDamage.GetValue() / 5);
            // move to starting position
            attacker.MoveToPosition(startingPosition, () =>
            {
                attacker.controller.EndAttack();
                attacker.onAttackComplete?.Invoke();
            });
        });
    }
}
