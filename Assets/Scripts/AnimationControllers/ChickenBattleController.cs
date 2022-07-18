using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBattleController : BattleController
{
    public override void StartAttack()
    {
        animator.SetBool("Run", true);
    }

    public override void Attack()
    {
        animator.SetBool("Run", false);
    }

    public override void Die()
    {
        animator.SetBool("Eat", true);
    }

    public override void TakeDamage()
    {
        animator.SetTrigger("Turn Head");
    }
}
