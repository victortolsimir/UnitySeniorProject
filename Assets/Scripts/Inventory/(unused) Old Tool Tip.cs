using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public Text toolTipText;
    public RectTransform backgroundRectTransform;

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowToolTip(string toolTipString)
    {
        gameObject.SetActive(true);
        toolTipText.text = toolTipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(toolTipText.preferredWidth+textPaddingSize*2f,toolTipText.preferredHeight+textPaddingSize*2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
        
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    
}
