using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using TMPro;

public class ResourceBar : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    [SerializeField]
    private TextMeshProUGUI text;

    private int currentValue;
    private int maxValue;

    public void SetCurrentValue(int value)
    {
        currentValue = value;
        if (currentValue < 0)
            currentValue = 0;
        UpdateUI();
    }

    public void SetMaxValue(int value)
    {
        maxValue = value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (maxValue > 0)
        {
            float ratio = currentValue / (float) maxValue;
            fill.color = gradient.Evaluate(ratio);
            fill.fillAmount = ratio;
        }
        if (text)
            text.text = $"{currentValue,5} / {maxValue}";
    }
}
