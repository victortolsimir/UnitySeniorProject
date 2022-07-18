using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookManager : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private GameObject healTab;
    [SerializeField]
    private GameObject attackTab;
    [SerializeField]
    private GameObject spellTemplate;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject spellInfoPanel;

    #endregion

    #region Fields

    private Transform currentTab;

    private HealSpell currentSpell;

    private GameObject selectedSpellBorder;

    private List<HealSpell> healSpells = new List<HealSpell>();
    private List<DamageSpell> damageSpells = new List<DamageSpell>();

    #endregion

    private void OpenSpellBook()
    {
        UpdateSpellsLists();
        OnHealTabButton();
        gameObject.SetActive(true);
    }

    private void SwitchTabs(Transform newTab)
    {
        if (currentTab)
            currentTab.Find("buttonToggle").gameObject.SetActive(false);
        currentTab = newTab;
        currentTab.Find("buttonToggle").gameObject.SetActive(true);
        spellInfoPanel.SetActive(false);
    }

    public void OnHealTabButton()
    {
        SwitchTabs(healTab.transform);
        ClearCurrentSpells();
        FillHealSpells();
    }

    private void FillHealSpells()
    {
        healSpells.ForEach(spell => { AddSpell(spell); });
    }

    public void OnAttackTabButton()
    {
        SwitchTabs(attackTab.transform);
        ClearCurrentSpells();
        FillAttackSpells();
    }

    private void FillAttackSpells()
    {
        damageSpells.ForEach(spell => { AddSpell(spell); });
    }

    private void AddSpell(Spell spell)
    {
        var spellTransform = Instantiate(spellTemplate, content).transform;
        spellTransform.name = spell.name;
        spellTransform.Find("SpellName").GetComponent<Text>().text = spell.name;
        spellTransform.gameObject.GetComponent<SpellContainer>().Spell = spell;
        spellTransform.GetComponent<Button>().onClick.AddListener(delegate { DisplaySpellInfo(spellTransform); });
        spellTransform.gameObject.SetActive(true);
    }

    private void DisplaySpellInfo(Transform spellTransform)
    {
        SetSpellBorder(spellTransform);

        if (spellInfoPanel.activeSelf)
            spellInfoPanel.SetActive(false);

        var spellInfoTransform = spellInfoPanel.transform;

        var spell = spellTransform.GetComponent<SpellContainer>().Spell;
        spellInfoTransform.Find("SpellName").GetComponent<Text>().text = spell.name;
        spellInfoTransform.Find("ManaCost").GetComponent<Text>().text = $"Mana Cost: {spell.cost}";
        spellInfoTransform.Find("Description").GetComponent<Text>().text = spell.description;

        var sb = new System.Text.StringBuilder();
        if (spell is HealSpell healSpell)
        {
            sb.AppendLine($"Base Heal Amount: {healSpell.BaseValue}");
            sb.AppendLine($"Heal Multiplier: x{healSpell.SpellMultiplier}");
            currentSpell = healSpell;
            spellInfoPanel.transform.Find("CastButton").gameObject.SetActive(true);
        }
        else if (spell is DamageSpell damageSpell)
        {
            sb.AppendLine($"Base Damage: {damageSpell.BaseValue}");
            sb.AppendLine($"Damage Multiplier: x{damageSpell.SpellMultiplier}");
            sb.AppendLine($"Magic Penetration: {damageSpell.magicPen * 100}%");
            spellInfoPanel.transform.Find("CastButton").gameObject.SetActive(false);
        }
        spellInfoTransform.Find("stats").GetComponent<Text>().text = sb.ToString();

        spellInfoPanel.SetActive(true);
    }

    public void OnCastSpellButton()
    {
        if (currentSpell)
        {
            GameObject.Find("Player").GetComponent<Character>().UseHealSpell(currentSpell);
            AmbientAudio.PlayThinkingSound();
        }
    }

    private void SetSpellBorder(Transform spellTransform)
    {
        if (selectedSpellBorder)
            selectedSpellBorder.SetActive(false);
        selectedSpellBorder = spellTransform.Find("toggleBorder").gameObject;
        selectedSpellBorder.SetActive(true);
    }

    private void ClearCurrentSpells()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateSpellsLists()
    {
        healSpells.Clear();
        damageSpells.Clear();
        var spells = GlobalControl.instance.spells;
        foreach (var spell in spells)
        {
            if (spell is HealSpell healSpell)
            {
                healSpells.Add(healSpell);
            }
            else if (spell is DamageSpell damageSpell)
            {
                damageSpells.Add(damageSpell);
            }
        }
    }

    public void OnSpellBookButton()
    {
        if (!gameObject.activeSelf)
            OpenSpellBook();
        else
            gameObject.SetActive(false);
    }
}
