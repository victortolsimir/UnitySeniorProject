using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : Attack
{
    [SerializeField]
    private int _requiredLevel = 0;
    public int RequiredLevel { get => _requiredLevel; }
}
