using System.Collections;
using System.Collections.Generic;

public class Damage
{
    public int PhysicalDamage { get; }
    public int MagicDamage { get; }
    public float PhysicalPen { get; }
    public float MagicPen { get; }

    public Damage(int physicalDamage, int magicDamage, float physicalPen, float magicPen)
    {
        PhysicalDamage = physicalDamage;
        MagicDamage = magicDamage;
        PhysicalPen = physicalPen;
        MagicPen = magicPen;
    }
}
