using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private CharacterSkills characterSkills;

    [SerializeField]
    private EquipmentSO equipment;

    [SerializeField]
    private Ability basicAttack;
    [SerializeField]
    private Ability onePunch;

    [SerializeField]
    private bool cheatAbility = false;

    private void Start()
    {
        characterSkills = GlobalControl.instance.characterSkills;
        RefreshAbilities();
    }

    public void RefreshAbilities()
    {
        ClearAbilities();
        Weapon weapon;
        if (weapon = equipment.currentEquipment[(int)EquipmentSlot.Weapon] as Weapon)
            UnlockWeaponAbilities(weapon);
        if (weapon = equipment.currentEquipment[(int)EquipmentSlot.Shield] as Weapon)
            UnlockWeaponAbilities(weapon);
        if (cheatAbility)
            GlobalControl.instance.abilities.Add(onePunch);
    }

    private void UnlockWeaponAbilities(Weapon weapon)
    {
        if (!weapon) return;
        var weaponSkill = FindWeaponSkill(weapon);
        if (weaponSkill is null) return;

        var playerAbilities = GlobalControl.instance.abilities;
        foreach (var ability in weapon.DefaultAbilities)
        {
            if (weaponSkill.Level >= ability.RequiredLevel && !playerAbilities.Contains(ability))
                playerAbilities.Add(ability);
        }
        foreach (var ability in weapon.uniqueAbilities)
        {
            if (weaponSkill.Level >= ability.RequiredLevel && !playerAbilities.Contains(ability))
                playerAbilities.Add(ability);
        }
    }

    private Skill FindWeaponSkill(Weapon weapon)
    {
        if (weapon is Sword)
            return characterSkills.swordSkill;
        else if (weapon is Staff)
            return characterSkills.staffSkill;
        else if (weapon is Shield)
            return characterSkills.shieldSkill;
        else if (weapon is Dagger)
            return characterSkills.daggerSkill;
        else
            return null;
    }

    private void ClearAbilities()
    {
        var abilities = GlobalControl.instance.abilities;
        abilities.Clear();
        abilities.Add(basicAttack);
    }
}
