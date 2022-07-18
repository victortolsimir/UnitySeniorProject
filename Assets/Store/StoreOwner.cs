using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreOwner : Interactable
{

    public bool isDeveloperStore = false;

    public string greeting;
    public GameObject StoreDialogueBox;

    [Header("Store attributes")]
    public Sprite StoreIcon;
    public string StoreName;
    
    public List<Item> storeItems;

    public override void Interact()
    {
        base.Interact();
        Store.instance.owner = this;
        ShowOptions();
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/npc_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void ShowOptions()
    {
        TextMeshProUGUI dialogue = StoreDialogueBox.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        dialogue.text = greeting;
        StoreDialogueBox.SetActive(true);
    }
}
