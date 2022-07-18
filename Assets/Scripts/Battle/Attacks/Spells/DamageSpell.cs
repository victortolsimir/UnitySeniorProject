using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Attack/Spell/DamageSpell")]
public class DamageSpell : Spell
{
    [Range(0, 1)]
    public float magicPen;

    [SerializeField]
    private GameObject projectilePrefab;

    public override void Activate(BattleCharacter attacker, BattleCharacter defender)
    {
        int magicDamage = BaseValue + (int)(attacker.stats.magicDamage.GetValue() * SpellMultiplier);
        if (IsCriticalHit(attacker))
        {
            int critDamage = attacker.stats.criticalDamage.GetValue();
            magicDamage += CalculateCritDamage(critDamage, magicDamage);
            attacker.ShowFloatingText("CRIT");
        }

        attacker.controller.CastOffensive();
        if (projectilePrefab)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, attacker.GetPosition(), attacker.transform.rotation);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.MoveToPosition(defender.GetPosition(), () =>
            {
                CompleteAttack(attacker, defender, magicDamage, magicPen);
            });
        }
        else
        {
            CompleteAttack(attacker, defender, magicDamage, magicPen);
        }
    }

    private void CompleteAttack(BattleCharacter attacker, BattleCharacter defender, int damage, float magicPen)
    {
        defender.TakeDamage(new Damage(0, damage, 0, magicPen));
        ApplyDebuffs(defender, attacker.stats.magicDamage.GetValue() / 5);
        attacker.onAttackComplete?.Invoke();
    }
}
