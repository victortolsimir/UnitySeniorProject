using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Misc/Spellbook")]
public class SpellBook : Misc
{
    [SerializeField]
    private Spell spell;

    //overrides item's DropItemObject method
    internal override void DropItemObject(Vector3 spawnPos)
    {
        Transform newObject = Instantiate(prefab.transform, spawnPos, Quaternion.identity);
        newObject.name = prefab.name;
        
        //assign this SO to to item pickup
        newObject.GetComponent<ItemPickup>().item = this;
    }

    public override void Use()
    {
        base.Use();
        var spells = GlobalControl.instance.spells;
        if (!spells.Contains(spell))
        {
            spells.Add(spell);
            ExperienceManager.AddMagicResistExperience(25);
        }
        else
        {
            ExperienceManager.AddMagicResistExperience(10);
        }
        AmbientAudio.PlayThinkingSound();
    }
}
