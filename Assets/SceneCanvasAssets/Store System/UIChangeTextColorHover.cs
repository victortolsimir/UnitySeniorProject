using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIChangeTextColorHover: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hover;
    public Color regular;
    public Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = regular;
    }

    private void OnDisable()
    {
        text.color = regular;
    }
}
