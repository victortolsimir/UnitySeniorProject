using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSymbolHandler : MonoBehaviour
{
    [SerializeField]
    private Sprite exclamationMark;

    [SerializeField]
    private Sprite questionMark;

    private SpriteRenderer spriteRenderer;

    private GameObject mainCamera;

    private bool QuestComplete { get; set; } = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        transform.LookAt(mainCamera.transform);
        var currentQuest = QuestController.instance.currentQuest;

        if (!currentQuest)
        {
            QuestComplete = false;
            spriteRenderer.sprite = exclamationMark;
        }
        else if (currentQuest.IsComplete())
        {
            spriteRenderer.sprite = questionMark;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }
}
