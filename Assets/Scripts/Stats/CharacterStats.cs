using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    [Header("Resources")]
    public Stat maxHealth;
    public int currentHealth;
    public Stat maxMana;
    public int currentMana;
    public Stat maxStamina;
    public int currentStamina;

    [Header("Offensive Stats"), Space(10)]
    public Stat physicalDamage;
    public Stat magicDamage;
    public Stat attackSpeed;
    public Stat criticalChance;
    public Stat criticalDamage;

    [Header("Defensive Stats"), Space(10)]
    public Stat physicalResist;
    public Stat magicResist;
}
