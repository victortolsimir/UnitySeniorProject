using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color selected = new Color(0.4258633f, 0.4838733f, 0.5471698f, 1f);
    public Color deselected = new Color(1, 1, 1, 1);

    public TabButton selectedTab;

    public List<GameObject> pages;

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        ChangeToColor(button,selected);
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < pages.Count; i++)
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }

    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            ChangeToColor(button,deselected);
        }
    }

    void ChangeToColor(TabButton button,Color type)
    {
        ColorBlock tabColors = button.GetComponent<Button>().colors;
        tabColors.normalColor = type;
        button.GetComponent<Button>().colors = tabColors;
    }
    

}
