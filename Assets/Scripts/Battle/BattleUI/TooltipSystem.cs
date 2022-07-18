using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    [SerializeField]
    private CustomTooltip tooltip;

    private void Awake()
    {
        instance = this;
    }

    public static void Show(string content, string header = "")
    {
        instance.tooltip.SetText(content, header);
        instance.tooltip.SetPosition();
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        if (instance && instance.tooltip)
            instance.tooltip.gameObject.SetActive(false);
    }
}
