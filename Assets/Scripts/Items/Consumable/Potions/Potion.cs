using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : Consumable
{
    public int restoreValue;

    public override string GetItemType() => "Potion";

    public override string UseAdjective() => "Drink";

    public override void Use()
    {
        base.Use();
        AmbientAudio.PlayDrinkSound();
    }
}
