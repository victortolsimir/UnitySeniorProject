using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomTooltip : MonoBehaviour
{
    [SerializeField]
    private Text headerField;
    [SerializeField]
    private Text contentField;
    [SerializeField]
    private LayoutElement layoutElement;
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private int characterWrapLimit;

    private void Update()
    {
        SetPosition();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = headerLength > characterWrapLimit || contentLength > characterWrapLimit;
    }

    public void SetPosition()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        pivotX = (pivotX <= 0.5) ? 0.03f : 0.97f;
        pivotY = (pivotY <= 0.5) ? 0.08f : 0.92f;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

}
