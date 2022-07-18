using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HealSpellManager : MonoBehaviour
{
    private Character player;
    [SerializeField]
    private GameObject healSpellPanel;
    private RectTransform content;
    [SerializeField]
    private GameObject prefabButton;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        content = healSpellPanel.GetComponent<ScrollRect>().content;
        AddHealSpells();
    }

    public void OnHealButton()
    {
        healSpellPanel.SetActive(!healSpellPanel.activeSelf);
    }

    private void OnHealAction(int index)
    {
        player.UseHealSpell(index);
    }

    public void AddHealSpells()
    {
        AddHealSpells(player.spells, OnHealAction);
    }

    private void AddHealSpells(List<Spell> spells, Action<int> action)
    {
        ClearItems();
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i] is HealSpell healSpell)
            {
                AddButtons($"{healSpell.name} ({healSpell.cost})", i, action);
            }
        }
    }

    private void ClearItems()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddButtons(string name, int index, Action<int> action)
    {
        GameObject newButton = Instantiate(prefabButton, content);
        newButton.name = name;
        newButton.GetComponentInChildren<Text>().text = name;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { action(index); });
        newButton.SetActive(true);
    }
}
