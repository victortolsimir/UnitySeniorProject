using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : Interactable
{
    [SerializeField]

    public string description = "";
    public string hint = "";

    public UniqueID progressID;
    public ExclusionList exclusionList;

    public Key keyItem;

    public List<GameObject> tiedObjects;

    [SerializeField]
    private InventorySO inventory;

    private void Start()
    {
        if (!exclusionList.exclusions.Contains(progressID))
        {
            foreach (GameObject item in tiedObjects)
            {
                item.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/inventory_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }


    public override void Interact()
    {
        if (!exclusionList.exclusions.Contains(progressID))
        {
            var keys = inventory.items["Misc"];
            if (keys.Contains(keyItem))
            {
                Inventory.instance.RemoveItem(keyItem);
                MessagePanelSystem.ShowMessage("Activated.");
                exclusionList.Add(progressID);
                foreach (GameObject item in tiedObjects)
                {
                    item.SetActive(true);
                }
                gameObject.SetActive(false);
            }
            else
            {
                MessagePanelSystem.ShowMessage(description + "\n" + hint);
            }
        }
    }
}
