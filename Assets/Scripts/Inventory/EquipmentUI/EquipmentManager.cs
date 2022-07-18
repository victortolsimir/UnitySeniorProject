using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    //delegate is an event you can subscribe different methods to
    public delegate void OnEquipChanged();
    public OnEquipChanged onEquipChangedCallback;

    public EquipmentSO equipmentSO;

    public GameObject SwordHand;
    public GameObject ShieldHand;

    public static EquipmentManager instance;
    private Character character;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager exists");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        character = GetComponent<Character>();
        EquipAllDefaults();
        EquipAllMeshes();
    }

    private void EquipAllDefaults()
    {
        //equip all defaults first
        foreach (DefaultItem item in equipmentSO.defaultItems())
        {
            if(!item.name.Equals("None"))
                item.Equip();
        }
    }

    public void EquipAllMeshes()
    {
        foreach (Equippable item in equipmentSO.currentEquipment)
        {
            if (item != null)
            {
                if (item.equipSlot == EquipmentSlot.Shield || item.equipSlot == EquipmentSlot.Weapon)
                    EquipHandItem((Weapon)item);
                else
                    EquipItem((Armor)item);
            }

        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------
    public void Equip(Equippable newItem)
    {
        ToggledInventorySlot currentToggled = GameObject.Find("InventoryManager").GetComponent<ToggledInventorySlot>();
        
        //keep original weight so add back the item's weight
        Inventory.instance.AdjustInventoryWeight(newItem.weight);
        
        //Remove it from inventory
        Inventory.instance.RemoveItem(newItem.GetItemType(), currentToggled.index);

        int slotIndex = (int)newItem.equipSlot;
        Equippable oldItem = equipmentSO.currentEquipment[slotIndex];
        Equippable mainHand = equipmentSO.currentEquipment[(int)EquipmentSlot.Weapon];
        Equippable offHand = equipmentSO.currentEquipment[(int)EquipmentSlot.Shield];

        bool removeOffHandMods = false;
        bool removeTwoHandedMods = false;

        if (newItem is Weapon weapon)
        {
            if (oldItem)
            {
                UnEquip(oldItem, true);
            }

            if (newItem is TwoHanded)
            {
                if (offHand)
                {
                    UnEquip(offHand, true);
                    removeOffHandMods = true;
                }
            }
            else if (slotIndex == (int)EquipmentSlot.Shield)
            {
                if (mainHand is TwoHanded)
                {
                    UnEquip(mainHand, true);
                    removeTwoHandedMods = true;
                }
            }

            equipmentSO.currentEquipment[slotIndex] = newItem;
            EquipHandItem(weapon);
            UpdateAbilities();
        }
        //any other type of equipment
        else
        {
            if (oldItem)
            {
                UnEquip(oldItem, true);
                RemoveItem((Armor)oldItem, false);
            }
            equipmentSO.currentEquipment[slotIndex] = newItem;
            EquipItem((Armor)newItem);
        }
       
        AddModifiers(newItem);
        if (oldItem) RemoveModifiers(oldItem);
        if (removeOffHandMods) RemoveModifiers(offHand);
        if (removeTwoHandedMods)
        {
            RemoveModifiers(mainHand);
        }

        //UI callback
        if (onEquipChangedCallback != null)
        {
            onEquipChangedCallback.Invoke();
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------

    public void UnEquip(Equippable item, bool swapped = false)
    {
        equipmentSO.currentEquipment[(int)item.equipSlot] = null;

        //add item back to inventory ignoring weight check
        Inventory.instance.AddItemFromEquipSlot(item);

        if (item is Weapon)
            RemoveHandItem(item.equipSlot);
        else
            RemoveItem((Armor)item, true);

        //UI callback
        if (onEquipChangedCallback != null)
        {
            onEquipChangedCallback.Invoke();
        }

        if (!swapped)
        {
            RemoveModifiers(item);
            UpdateAbilities();
        }
    }
    //--------------------------------------------------------------------------------------------------------------------------------

    private void EquipItem(Armor newItem)
    {
        UnequipItemDefaults(newItem);

        if(newItem is CustomEquippable)
        {
            CustomEquip((CustomEquippable)newItem);
            return;
        }
        
        bool male = GlobalControl.instance.isMale;
        ArmorColorBlock overrideColor = newItem.armorColorBlock;
        Transform pieces;

        if (!male && newItem.femaleItemPieces != null) //If player is female and armor has female pieces
            pieces = newItem.femaleItemPieces.transform;
        else
            pieces = newItem.itemPieces.transform;

        //enable dynamic bone script for cape
        if(newItem.isCape)
        {
            Transform item = FindItemFromParent(pieces.GetChild(0).name);
            item.GetComponent<DynamicBone>().enabled = true;
        }

        foreach (Transform piece in pieces)
        {
            Transform item = FindItemFromParent(piece.name);
            
            if(overrideColor != null)
            {
                MaterialPropertyBlock overrideBlock = overrideColor.GetMaterialPropertyBlock();

                Color[] colorPrefs = GlobalControl.instance.equipment.defaultColorPref;
                //add skin color to MaterialPropertyBlock
                if (colorPrefs != null && colorPrefs.Length != 0)
                {
                    
                    Color skinColor = GlobalControl.instance.equipment.defaultColorPref[1];
                    overrideBlock.SetColor("_Color_Skin", GlobalControl.instance.equipment.defaultColorPref[1]);
                }
                item.GetComponent<Renderer>().SetPropertyBlock(overrideBlock);
            }
                
            item.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }

    private void RemoveItem(Armor oldItem, bool addDefault)
    {
        if (oldItem is CustomEquippable)
        {
            CustomUnEquip((CustomEquippable)oldItem);
            if (addDefault) AddDefaultItems(oldItem);
            return;
        }

        bool male = GlobalControl.instance.isMale;
        ArmorColorBlock overrideColor = oldItem.armorColorBlock;
        Transform pieces;

        //If player is female and armor has female pieces
        if (!male && oldItem.femaleItemPieces != null)
            pieces = oldItem.femaleItemPieces.transform;
        else
            pieces = oldItem.itemPieces.transform;

        //disable dynamic bone script for cape
        if (oldItem.isCape)
        {
            Transform item = FindItemFromParent(pieces.GetChild(0).name);
            item.GetComponent<DynamicBone>().enabled = false;
        }


        foreach (Transform piece in pieces)
        {
            Transform item = FindItemFromParent(piece.name);
            
            if (overrideColor != null)
                item.GetComponent<Renderer>().SetPropertyBlock(null);
            item.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }

        if (addDefault)
        {
            AddDefaultItems(oldItem);
        }
    }

    private void AddDefaultItems(Armor oldItem)
    {
        switch (oldItem.equipSlot)
        {
            case EquipmentSlot.Chest:
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.Body]);
                break;
            case EquipmentSlot.Feet:
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.Feet]);
                break;
            case EquipmentSlot.Gloves:
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.Hands]);
                break;
            case EquipmentSlot.Legs:
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.Legs]);
                break;
            case EquipmentSlot.Head:
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.Hair]);
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.Head]);
                EquipDefaultItem(equipmentSO.defaultItems()[(int)DefaultType.FacialHair]);
                break;

        }
    }

    private Transform FindItemFromParent(string name)
    {
        var children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == name)
                return child;
        return null;
    }
    private void EquipDefaultItem(DefaultItem item)
    {
        if (!item.name.Equals("None"))
            item.Equip();
    }

    private void UnequipItemDefaults(Equippable item)
    {
        Armor armorItem = (Armor)item;
        bool[] disables = armorItem.GetDefaultDisables();

        for (int i = 0; i < disables.Length; i++)
        {
            if (disables[i] && !equipmentSO.defaultItems()[i].name.Equals("None"))
                equipmentSO.defaultItems()[i].UnEquip();
        }

    }

    void EquipHandItem(Weapon newItem)
    {
        //equip item mesh onto player
        MeshRenderer newMesh = Instantiate<MeshRenderer>(newItem.mesh.GetComponent<MeshRenderer>());
        if (newItem.equipSlot == EquipmentSlot.Weapon)
        {
            newMesh.transform.parent = SwordHand.transform;
            newMesh.transform.localPosition = newItem.mesh.transform.localPosition;
            newMesh.transform.localRotation = newItem.mesh.transform.localRotation;
        }
        else if (newItem.equipSlot == EquipmentSlot.Shield)
        {
            newMesh.transform.parent = ShieldHand.transform;
            newMesh.transform.localPosition = newItem.mesh.transform.localPosition;
            newMesh.transform.localRotation = newItem.mesh.transform.localRotation;
        }
    }

    void RemoveHandItem(EquipmentSlot slot)
    {
        switch (slot)
        {
            case EquipmentSlot.Weapon:
                Destroy(SwordHand.GetComponentInChildren<MeshRenderer>().gameObject);
                break;
            case EquipmentSlot.Shield:
                Destroy(ShieldHand.GetComponentInChildren<MeshRenderer>().gameObject);
                break;

        }
    }

    private void AddModifiers(Equippable item)
    {
        var stats = item.stats;

        character.AddMaxHealthMod(stats.currentHealth);
        character.AddMaxManaMod(stats.currentMana);
        character.AddMaxStaminaMod(stats.currentStamina);

        character.AddPhysicalDamageMod(stats.physicalDamage.GetValue());
        character.AddMagicDamageMod(stats.magicDamage.GetValue());
        character.AddAttackSpeedMod(stats.attackSpeed.GetValue());
        character.AddCriticalChanceMod(stats.criticalChance.GetValue());
        character.AddCriticalDamageMod(stats.criticalDamage.GetValue());

        character.AddPhysicalResistMod(stats.physicalResist.GetValue());
        character.AddMagicResistMod(stats.magicResist.GetValue());
    }

    private void RemoveModifiers(Equippable item)
    {
        var stats = item.stats;

        character.RemoveMaxHealthMod(stats.currentHealth);
        character.RemoveMaxManaMod(stats.currentMana);
        character.RemoveMaxStaminaMod(stats.currentStamina);

        character.RemovePhysicalDamageMod(stats.physicalDamage.GetValue());
        character.RemoveMagicDamageMod(stats.magicDamage.GetValue());
        character.RemoveAttackSpeedMod(stats.attackSpeed.GetValue());
        character.RemoveCriticalChanceMod(stats.criticalChance.GetValue());
        character.RemoveCriticalDamageMod(stats.criticalDamage.GetValue());

        character.RemovePhysicalResistMod(stats.physicalResist.GetValue());
        character.RemoveMagicResistMod(stats.magicResist.GetValue());
    }

    private void UpdateAbilities()
    {
        GetComponent<AbilityManager>().RefreshAbilities();
    }

    private void CustomEquip(CustomEquippable item)
    {
        Transform parent = FindItemFromParent(item.parentBone);
        Transform itemMesh = item.mesh.transform;
        
        MeshRenderer newMesh = Instantiate<MeshRenderer>(itemMesh.GetComponent<MeshRenderer>());
        newMesh.transform.parent = parent.transform;
        newMesh.transform.localPosition = itemMesh.localPosition;
        newMesh.transform.localRotation = itemMesh.localRotation;

    }

    private void CustomUnEquip(CustomEquippable item)
    {
        Transform parent = FindItemFromParent(item.parentBone);
        Destroy(parent.GetComponentInChildren<MeshRenderer>().gameObject);

    }
}
