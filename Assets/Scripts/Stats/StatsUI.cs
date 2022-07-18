
using System;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField]
    private Text names;
    [SerializeField]
    private Text values;
    private CharacterStats stats;
    [SerializeField]
    private GameObject statsPanel;
    
    void Start()
    {
        // stats = GameObject.Find("Player").GetComponent<Character>().stats;
        stats = GlobalControl.instance.stats;
    }

    void Update()
    {
        if (Input.GetButtonDown("StatsUI"))
        {
            statsPanel.SetActive(!statsPanel.activeSelf);
        }

        var sbNames = new StringBuilder();
        var sbValues = new StringBuilder();

        sbNames.AppendLine("Health:");
        sbValues.AppendLine($"{stats.currentHealth} / {stats.maxHealth.GetValue()}");

        sbNames.AppendLine("Mana:");
        sbValues.AppendLine($"{stats.currentMana} / {stats.maxMana.GetValue()}");

        sbNames.AppendLine("Stamina:");
        sbValues.AppendLine($"{stats.currentStamina} / {stats.maxStamina.GetValue()}");

        sbNames.AppendLine("Physical Damage:");
        sbValues.AppendLine($"{stats.physicalDamage.GetValue()}");
        
        sbNames.AppendLine("Magic Damage:");
        sbValues.AppendLine($"{stats.magicDamage.GetValue()}");
        
        sbNames.AppendLine("Attack Speed:");
        sbValues.AppendLine($"{stats.attackSpeed.GetValue()}");

        sbNames.AppendLine("Critical Chance:");
        sbValues.AppendLine($"{stats.criticalChance.GetValue()}");

        sbNames.AppendLine("Critical Damage:");
        sbValues.AppendLine($"{stats.criticalDamage.GetValue()}");

        sbNames.AppendLine("Physical Resist:");
        sbValues.AppendLine($"{stats.physicalResist.GetValue()}");

        sbNames.AppendLine("Magic Resist:");
        sbValues.AppendLine($"{stats.magicResist.GetValue()}");

        names.text = sbNames.ToString();
        values.text = sbValues.ToString();
    }

    public void OpenStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void CloseStatsPanel()
    {
        statsPanel.SetActive(false);
    }
}
