using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : Attack
{
    [SerializeField]
    private float _spellMultiplier = 0;

    public float SpellMultiplier { get => _spellMultiplier; }

    [SerializeField]
    private int _baseValue = 0;

    public int BaseValue { get => _baseValue; }
}
