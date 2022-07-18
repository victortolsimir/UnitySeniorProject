using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum EquipmentSlot {Head,Chest,Gloves,Legs,Feet,Weapon,Shield,Misc}
public abstract class Equippable : Item
{
    [Header("Equip Slot")]
    public EquipmentSlot equipSlot;
    public CharacterStats stats;

    public override string UseAdjective() => "Equip";

    public override void Use()
    {
        base.Use();
        //Equip the item
        EquipmentManager.instance.Equip(this);
    }

    public virtual void UnEquip()
    {
        EquipmentManager.instance.UnEquip(this);
    }

    public override string GetStats()
    {
        var sb = new StringBuilder(base.GetStats());
        int value;
        if ((value = stats.currentHealth) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Health");
        if ((value = stats.currentMana) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Mana");
        if ((value = stats.currentStamina) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Stamina");
        if ((value = stats.physicalDamage.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Physical Damage");
        if ((value = stats.magicDamage.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Magic Damage");
        if ((value = stats.attackSpeed.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Attack Speed");
        if ((value = stats.criticalChance.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Critical Chance");
        if ((value = stats.criticalDamage.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Critical Damage");
        if ((value = stats.physicalResist.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Physical Resist");
        if ((value = stats.magicResist.GetValue()) != 0)
            sb.AppendLine($"{StatSign(value)}{value} Magic Resist");

        var str = sb.ToString();
        return string.IsNullOrWhiteSpace(str) ? "None" : str;
    }

    private string StatSign(int value)
    {
        if (value >= 0)
            return "+";
        else
            return "";
    }
}



