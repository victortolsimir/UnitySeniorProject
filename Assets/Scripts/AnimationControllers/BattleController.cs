using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour, IBattleController
{
    protected Animator animator;

    [SerializeField]
    private string startAttack = "";
    [SerializeField]
    private string attack = "";
    [SerializeField]
    private string endAttack = "";
    [SerializeField]
    private string castOffensive = "";
    [SerializeField]
    private string castDefensive = "";
    [SerializeField]
    private string takeDamage = "Take Damage";
    [SerializeField]
    private string die = "Die";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public virtual void StartAttack()
    {
        TriggerAnimation(startAttack);
    }

    public virtual void Attack()
    {
        TriggerAnimation(attack);
    }

    public virtual void EndAttack()
    {
        TriggerAnimation(endAttack);
    }

    public virtual void CastOffensive()
    {
        TriggerAnimation(castOffensive);
    }

    public virtual void CastDefensive()
    {
        TriggerAnimation(castDefensive);
    }

    public virtual void TakeDamage()
    {
        TriggerAnimation(takeDamage);
    }

    public virtual void Die()
    {
        TriggerAnimation(die);
    }

    private void TriggerAnimation(string triggerName)
    {
        if (!string.IsNullOrWhiteSpace(triggerName)) animator.SetTrigger(triggerName);
    }
}
