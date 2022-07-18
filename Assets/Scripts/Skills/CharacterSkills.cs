using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSkills
{
    [Header("Weapon Skills")]
    public Skill swordSkill = new SwordSkill();
    public Skill staffSkill = new StaffSkill();
    public Skill shieldSkill = new ShieldSkill();
    public Skill daggerSkill = new DaggerSkill();

    [Header("Stat Skills")]
    public Skill vitality = new StatSkill("Health");
    public Skill mind = new StatSkill("Mana");
    public Skill endurance = new StatSkill("Stamina");
    public Skill toughness = new StatSkill("Physical Resist");
    public Skill wisdom = new StatSkill("Magic Resist");
}
