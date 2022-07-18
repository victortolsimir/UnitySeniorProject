using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusEffectManager))]
public class BattleCharacter : Character
{
    private Vector3 moveTargetPosition;
    private Action onAnimComplete;
    public Action onAttackComplete = null;
    public Action onAttackFail = null;
    private bool moving = false;

    private StatusEffectManager statusEffectManager;

    private new void Start()
    {
        if (CompareTag("Player"))
        {
            healthBar = GameObject.Find("PlayerHealthBar").GetComponent<ResourceBar>();
            manaBar = GameObject.Find("PlayerManaBar").GetComponent<ResourceBar>();
            staminaBar = GameObject.Find("PlayerStaminaBar").GetComponent<ResourceBar>();
        }
        else
        {
            healthBar = GameObject.Find("EnemyHealthBar").GetComponent<ResourceBar>();
        }
        statusEffectManager = GetComponent<StatusEffectManager>();
        base.Start();
    }

    private void Update()
    {
        if (moving)
        {
            float animSpeed = 5f;
            transform.position += (moveTargetPosition - GetPosition()) * animSpeed * Time.deltaTime;

            float reachedDistance = 0.5f;
            if (Vector3.Distance(GetPosition(), moveTargetPosition) < reachedDistance)
            {
                transform.position = moveTargetPosition;
                moving = false;
                onAnimComplete();
            }
        }
    }

    public void UseAbility(int choice, BattleCharacter enemy)
    {
        Ability ability = abilities[choice];
        
        if (CompareTag("Player"))
        {
            var abilityCost = ability.cost;
            if (abilityCost > stats.currentStamina)
            {
                ShowFloatingText("Not enough stamina");
                onAttackFail?.Invoke();
                return;
            }

            LoseStamina(abilityCost);

            if (abilityCost == 0)
            {
                ExperienceManager.AddWeaponExperience(5);
            }
            else
            {
                ExperienceManager.AddWeaponExperience(abilityCost);
                ExperienceManager.AddStaminaExperience(abilityCost);
            }
        }
        
        if (audioController)
        {
            audioController.PlayAttackSound();
        }
        ability.Activate(this, enemy);
    }

    public void UseSpell(int choice, BattleCharacter enemy)
    {
        Spell spell = spells[choice];
        if (CompareTag("Player"))
        {
            var spellCost = spell.cost;
            if (spellCost > stats.currentMana)
            {
                ShowFloatingText("Not enough mana");
                onAttackFail?.Invoke();
                return;
            }

            LoseMana(spellCost);
        }

        if (audioController)
        {
            audioController.PlaySpellSound();
        }
        spell.Activate(this, enemy);
    }

    public void MoveToPosition(Vector3 moveTargetPosition, Action onAnimComplete)
    {
        this.moveTargetPosition = moveTargetPosition;
        this.onAnimComplete = onAnimComplete;
        moving = true;
    }

    public void HandleEffects() => statusEffectManager?.HandleEffects();

    public void ClearEffects() => statusEffectManager?.ClearEffects();
}
