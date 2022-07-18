using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private static ExperienceManager instance;

    private CharacterSkills characterSkills;

    [SerializeField]
    private EquipmentSO equipment;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    private void Start()
    {
        characterSkills = GlobalControl.instance.characterSkills;
    }

    public static void AddWeaponExperience(int value)
    {
        if (!instance) return;
        if (value < 1) return;

        Weapon weapon;
        if (weapon = instance.equipment.currentEquipment[(int)EquipmentSlot.Weapon] as Weapon)
        {
            if (weapon is Sword)
                instance.AddSwordExperience(value);
            else if (weapon is Staff)
                instance.AddStaffExperience(value);
        }

        if (weapon = instance.equipment.currentEquipment[(int)EquipmentSlot.Shield] as Weapon)
        {
            if (weapon is Shield)
                instance.AddShieldExperience(value);
            else if (weapon is Dagger)
                instance.AddDaggerExperience(value);
        }
    }

    private void AddSwordExperience(int value)
    {
        characterSkills.swordSkill.AddExperience(value);
    }

    private void AddStaffExperience(int value)
    {
        characterSkills.staffSkill.AddExperience(value);
    }

    private void AddShieldExperience(int value)
    {
        characterSkills.shieldSkill.AddExperience(value);
    }

    private void AddDaggerExperience(int value)
    {
        characterSkills.daggerSkill.AddExperience(value);
    }

    public static void AddHealthExperience(int value)
    {
        if (instance && value > 0)
            instance.characterSkills.vitality.AddExperience(value);
    }

    public static void AddManaExperience(int value)
    {
        if (instance && value > 0)
            instance.characterSkills.mind.AddExperience(value);
    }

    public static void AddStaminaExperience(int value)
    {
        if (instance && value > 0)
            instance.characterSkills.endurance.AddExperience(value);
    }

    public static void AddPhysicalResistExperience(int value)
    {
        if (instance && value > 0)
            instance.characterSkills.toughness.AddExperience(value);
    }

    public static void AddMagicResistExperience(int value)
    {
        if (instance && value > 0)
            instance.characterSkills.wisdom.AddExperience(value);
    }
}
