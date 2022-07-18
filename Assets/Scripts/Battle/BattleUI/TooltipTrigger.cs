using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string content;

    private IEnumerator coroutine;

    private void OnDisable()
    {
        OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        coroutine = TooltipDelay();
        StartCoroutine(coroutine);
        // TooltipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        TooltipSystem.Hide();
    }

    private IEnumerator TooltipDelay()
    {
        yield return new WaitForSeconds(0.5f);
        TooltipSystem.Show(content, header);
    }
}
