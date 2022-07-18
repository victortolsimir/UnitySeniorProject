using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject arrow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        arrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        arrow.SetActive(false);
    }
}
