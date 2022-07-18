using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Misc item")]
public class Misc : Item
{
    public override string GetItemType() => "Misc";
    public override string UseAdjective() => "Use";

    public override string GetStats()
    {
        string result = base.GetStats();
        result += "None discovered.";
        return result;
    }
}
