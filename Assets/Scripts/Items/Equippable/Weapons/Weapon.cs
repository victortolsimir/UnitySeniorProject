using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Equippable
{
    public GameObject mesh;

    [SerializeField]
    private DefaultWeaponAbilities _defaultAbilities;
    public List<Ability> DefaultAbilities { get => _defaultAbilities.abilities; }

    public List<Ability> uniqueAbilities = new List<Ability>();

    public override string GetItemType() => "Weapon";
}
