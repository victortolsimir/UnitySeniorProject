using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject abilityPanel;
    public GameObject spellPanel;
    public GameObject itemPanel;
    public GameObject battleOutcome;
    public GameObject prefabButton;

    public GameObject PlayerActionText;
    public GameObject EnemyActionText;

    private Action OnEscape;

    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnButton(GameObject panel)
    {
        bool activeState = panel.activeSelf;

        DisableExtraPanels();
        panel.SetActive(!activeState);
    }

    public void OnAbilitiesButton()
    {
        OnButton(abilityPanel);
    }

    public void OnSpellsButton()
    {
        OnButton(spellPanel);
    }

    public void OnItemsButton()
    {
        OnButton(itemPanel);
    }

    public void OnEscapeButton()
    {
        OnEscape?.Invoke();
    }

    public void SetEscapeButton(Action action)
    {
        OnEscape = action;
    }

    public void DisableExtraPanels()
    {
        abilityPanel.SetActive(false);
        spellPanel.SetActive(false);
        itemPanel.SetActive(false);
    }

    public void OnVictory()
    {
        battleOutcome.GetComponent<Text>().text = "VICTORY!";
        battleOutcome.SetActive(true);
    }

    public void AddAbilites(List<Ability> abilities, Action<int> action)
    {
        var content = abilityPanel.GetComponent<ScrollRect>().content;
        for (int i = 0; i < abilities.Count; i++)
        {
            var ability = abilities[i];
            AddButtons(content, $"{ability.name} ({ability.cost})", ability.description, i, action);
        }
    }

    public void AddSpells(List<Spell> spells, Action<int> action)
    {
        var content = spellPanel.GetComponent<ScrollRect>().content;
        for (int i = 0; i < spells.Count; i++)
        {
            var spell = spells[i];
            AddButtons(content, $"{spell.name} ({spell.cost})", spell.description, i, action);
        }
    }

    //public void AddItems(List<Item> items, Action<int> action)
    //{
    //    var content = itemPanel.GetComponent<ScrollRect>().content;
    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        var item = items[i];
    //        AddButtons(content, item.name, item.description, i, action);
    //    }
    //}

    public void AddItems(List<Item> items, Action<Item> action)
    {
        var itemsWithCount = new List<(Item item, int count)>();

        foreach (var item in items)
        {
            bool itemFound = false;
            for (int i = 0; i < itemsWithCount.Count; i++)
            {
                if (item == itemsWithCount[i].item)
                {
                    itemsWithCount[i] = (item, itemsWithCount[i].count + 1);
                    itemFound = true;
                    break;
                }
            }
            if (!itemFound) itemsWithCount.Add((item, 1));
        }

        foreach (var (item, count) in itemsWithCount)
        {
            AddItemButtons(item, count, action);
        }
    }

    private void AddItemButtons(Item item, int count, Action<Item> action)
    {
        var name = $"{item.name} ({count})";
        var content = itemPanel.GetComponent<ScrollRect>().content;
        GameObject newButton = Instantiate(prefabButton, content);
        newButton.name = name;
        newButton.GetComponentInChildren<Text>().text = name;
        var trigger = newButton.AddComponent<TooltipTrigger>();
        trigger.header = name;
        trigger.content = item.description;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { action(item); });
        newButton.SetActive(true);
    }

    public void ClearItems()
    {
        foreach (Transform child in itemPanel.GetComponent<ScrollRect>().content)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddButtons(Transform content, string name, string description, int index, Action<int> action)
    {
        GameObject newButton = Instantiate(prefabButton, content);
        newButton.name = name;
        newButton.GetComponentInChildren<Text>().text = name;
        var trigger = newButton.AddComponent<TooltipTrigger>();
        trigger.header = name;
        trigger.content = description;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { action(index); });
        newButton.SetActive(true);
    }

    public void OnDefeat()
    {
        battleOutcome.GetComponent<Text>().text = "DEFEAT!";
        battleOutcome.SetActive(true);
    }

    public void UpdateActionText(int playerCount, int enemyCount)
    {
        PlayerActionText.GetComponent<Text>().text = $"Player Actions: {playerCount}";
        EnemyActionText.GetComponent<Text>().text = $"Enemy Actions: {enemyCount}";
    }
}
