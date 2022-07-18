using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Character : MonoBehaviour
{
    public CharacterStats stats;

    [Header("Resource Bars"), Space(10)]
    public ResourceBar healthBar;
    public ResourceBar manaBar;
    public ResourceBar staminaBar;

    [Space(10)]
    public List<Ability> abilities;
    public List<Spell> spells;

    [Space(10)]
    public GameObject floatingTextPrefab;

    public IBattleController controller;
    protected AudioSourceEffectController audioController;

    private void Awake()
    {
        controller = GetComponent<IBattleController>();
        if (CompareTag("Player"))
        {
            LoadPlayer();
        }
    }

    public void Start()
    {
        healthBar?.SetMaxValue(stats.maxHealth.GetValue());
        healthBar?.SetCurrentValue(stats.currentHealth);
        manaBar?.SetMaxValue(stats.maxMana.GetValue());
        manaBar?.SetCurrentValue(stats.currentMana);
        staminaBar?.SetMaxValue(stats.maxStamina.GetValue());
        staminaBar?.SetCurrentValue(stats.currentStamina);

        audioController = GetComponent<AudioSourceEffectController>();
    }

    private void OnDestroy()
    {
        if (CompareTag("Player"))
        {
            SavePlayer();
        }
    }

    public Vector3 GetPosition() => transform.position;

    public void TakeDamage(Damage incomingDamage)
    {
        int effectivePhysicalResist = (int)(stats.physicalResist.GetValue() * (1 - incomingDamage.PhysicalPen));
        int effectiveMagicResist = (int)(stats.magicResist.GetValue() * (1 - incomingDamage.MagicPen));

        int physicalDamage = CalcIncomingDamage(incomingDamage.PhysicalDamage, effectivePhysicalResist);
        int magicDamage = CalcIncomingDamage(incomingDamage.MagicDamage, effectiveMagicResist);

        if (CompareTag("Player"))
        {
            ExperienceManager.AddPhysicalResistExperience(physicalDamage);
            ExperienceManager.AddMagicResistExperience(magicDamage);
            ExperienceManager.AddHealthExperience(physicalDamage + magicDamage);
        }

        TakeDamage(physicalDamage + magicDamage);
    }

    private int CalcIncomingDamage(int damage, int effectiveResist)
    {
        int resist = (int)(Math.Sqrt(effectiveResist * 2) + (effectiveResist / 10));
        if (resist > 80)
            resist = 80;

        return (int)(damage * (1 - (float)resist / 100));
    }

    private void TakeDamage(int damage)
    {
        if (damage < 0) damage = 0;
        stats.currentHealth -= damage;
        Debug.Log($"{name} takes {damage} damage.");
        healthBar?.SetCurrentValue(stats.currentHealth);
        controller.TakeDamage();
        if (floatingTextPrefab)
        {
            ShowFloatingText(damage.ToString(), Color.red);
        }

        if (IsDead())
        {
            Die();
        }
        else if (audioController)
        {
            audioController.PlayDamageTakenSound();
        }
    }

    public bool IsDead() => stats.currentHealth <= 0;

    private void Die()
    {
        Debug.Log($"{name} has died.");
        controller.Die();
        if (audioController)
        {
            audioController.PlayDeathSound();
        }
    }

    public void LoseStamina(int cost)
    {
        stats.currentStamina -= cost;
        if (stats.currentStamina < 0)
            stats.currentStamina = 0;
        staminaBar?.SetCurrentValue(stats.currentStamina);
    }

    public void LoseMana(int cost)
    {
        stats.currentMana -= cost;
        if (stats.currentMana < 0)
            stats.currentMana = 0;
        manaBar?.SetCurrentValue(stats.currentMana);
        if (CompareTag("Player"))
        {
            ExperienceManager.AddManaExperience(cost);
            if (GetComponent<EquipmentManager>().equipmentSO.currentEquipment[(int)EquipmentSlot.Weapon] is Staff)
            {
                ExperienceManager.AddWeaponExperience(cost);
            }
        }
    }

    public void GainHealth(int healingAmount)
    {
        stats.currentHealth += healingAmount;
        Debug.Log($"{transform.name} heals {healingAmount} health.");
        if (stats.currentHealth > stats.maxHealth.GetValue())
            stats.currentHealth = stats.maxHealth.GetValue();
        healthBar?.SetCurrentValue(stats.currentHealth);
        if (floatingTextPrefab)
        {
            ShowFloatingText(healingAmount.ToString(), Color.green);
        }
    }

    public void GainMana(int manaAmount)
    {
        stats.currentMana += manaAmount;
        Debug.Log($"{transform.name} gains {manaAmount} mana.");
        if (stats.currentMana > stats.maxMana.GetValue())
            stats.currentMana = stats.maxMana.GetValue();
        manaBar?.SetCurrentValue(stats.currentMana);
        if (floatingTextPrefab)
        {
            ShowFloatingText(manaAmount.ToString(), Color.blue);
        }
    }

    public void GainStamina(int staminaAmount)
    {
        stats.currentStamina += staminaAmount;
        Debug.Log($"{transform.name} gains {staminaAmount} stamina.");
        if (stats.currentStamina > stats.maxStamina.GetValue())
            stats.currentStamina = stats.maxStamina.GetValue();
        staminaBar?.SetCurrentValue(stats.currentStamina);
        if (floatingTextPrefab)
        {
            ShowFloatingText(staminaAmount.ToString(), Color.yellow);
        }
    }

    public void ShowFloatingText(string text, Color color)
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        var textMesh = go.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
    }

    public void ShowFloatingText(string text)
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        var textMesh = go.GetComponent<TextMesh>();
        textMesh.text = text;
    }

    public void UseHealSpell(int choice)
    {
        Spell spell = spells[choice];
        if (spell is HealSpell healSpell)
        {
            if (healSpell.cost > stats.currentMana)
            {
                ShowFloatingText("Not enough mana");
                return;
            }
            LoseMana(healSpell.cost);
            healSpell.ActivateOutOfBattle(this);
        }
        else
        {
            Debug.LogError("Attempted to cast non-heal spell as heal spell");
        }
    }

    public void UseHealSpell(Spell spell)
    {
        UseHealSpell(spells.IndexOf(spell));
    }

    public void SavePlayer()
    {
        // GlobalControl.instance.stats = stats;
        if (GlobalControl.instance.playerstate is GlobalControl.PlayerState.EnteringBattle)
        {
            GlobalControl.instance.playerPosition = transform.position;
        }
    }

    private void LoadPlayer()
    {
        stats = GlobalControl.instance.stats;
        abilities = GlobalControl.instance.abilities;
        spells = GlobalControl.instance.spells;
        if (GlobalControl.instance.playerstate is GlobalControl.PlayerState.ReturningFromBattle || GlobalControl.instance.playerstate is GlobalControl.PlayerState.Loading)
        {
            transform.position = GlobalControl.instance.playerPosition;
            GlobalControl.instance.playerstate = GlobalControl.PlayerState.Standard;
        }
    }

    #region Stat Modifiers
    // Health
    public void AddMaxHealthMod(int value)
    {
        stats.maxHealth.AddModifier(value);
        int max = stats.maxHealth.GetValue();
        if (stats.currentHealth > max)
        {
            stats.currentHealth = max;
        }
        healthBar?.SetMaxValue(stats.maxHealth.GetValue());
        healthBar?.SetCurrentValue(stats.currentHealth);
    }

    public void RemoveMaxHealthMod(int value)
    {
        stats.maxHealth.RemoveModifier(value);
        int max = stats.maxHealth.GetValue();
        if (stats.currentHealth > max)
        {
            stats.currentHealth = max;
        }
        healthBar?.SetMaxValue(stats.maxHealth.GetValue());
        healthBar?.SetCurrentValue(stats.currentHealth);
    }

    // Mana
    public void AddMaxManaMod(int value)
    {
        stats.maxMana.AddModifier(value);
        int max = stats.maxMana.GetValue();
        if (stats.currentMana > max)
        {
            stats.currentMana = max;
        }
        manaBar?.SetMaxValue(stats.maxMana.GetValue());
        manaBar?.SetCurrentValue(stats.currentMana);
    }

    public void RemoveMaxManaMod(int value)
    {
        stats.maxMana.RemoveModifier(value);
        int max = stats.maxMana.GetValue();
        if (stats.currentMana > max)
        {
            stats.currentMana = max;
        }
        manaBar?.SetMaxValue(stats.maxMana.GetValue());
        manaBar?.SetCurrentValue(stats.currentMana);
    }

    // Stamina
    public void AddMaxStaminaMod(int value)
    {
        stats.maxStamina.AddModifier(value);
        int max = stats.maxStamina.GetValue();
        if (stats.currentStamina > max)
        {
            stats.currentStamina = max;
        }
        staminaBar?.SetMaxValue(stats.maxStamina.GetValue());
        staminaBar?.SetCurrentValue(stats.currentStamina);
    }

    public void RemoveMaxStaminaMod(int value)
    {
        stats.maxStamina.RemoveModifier(value);
        int max = stats.maxStamina.GetValue();
        if (stats.currentStamina > max)
        {
            stats.currentStamina = max;
        }
        staminaBar?.SetMaxValue(stats.maxStamina.GetValue());
        staminaBar?.SetCurrentValue(stats.currentStamina);
    }

    // Physical Damage
    public void AddPhysicalDamageMod(int value) => stats.physicalDamage.AddModifier(value);

    public void RemovePhysicalDamageMod(int value) => stats.physicalDamage.RemoveModifier(value);

    // Magic Damage
    public void AddMagicDamageMod(int value) => stats.magicDamage.AddModifier(value);

    public void RemoveMagicDamageMod(int value) => stats.magicDamage.RemoveModifier(value);

    // Attack Speed
    public void AddAttackSpeedMod(int value) => stats.attackSpeed.AddModifier(value);

    public void RemoveAttackSpeedMod(int value) => stats.attackSpeed.RemoveModifier(value);

    // Physical Resist
    public void AddPhysicalResistMod(int value) => stats.physicalResist.AddModifier(value);

    public void RemovePhysicalResistMod(int value) => stats.physicalResist.RemoveModifier(value);

    // Magic Resist
    public void AddMagicResistMod(int value) => stats.magicResist.AddModifier(value);

    public void RemoveMagicResistMod(int value) => stats.magicResist.RemoveModifier(value);

    // Critical Chance
    public void AddCriticalChanceMod(int value) => stats.criticalChance.AddModifier(value);

    public void RemoveCriticalChanceMod(int value) => stats.criticalChance.RemoveModifier(value);

    // Critical Damage
    public void AddCriticalDamageMod(int value) => stats.criticalDamage.AddModifier(value);

    public void RemoveCriticalDamageMod(int value) => stats.criticalDamage.RemoveModifier(value);
    #endregion
}
