using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBeerItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    public override void ChangeCursor()
    {
        var cursor = Resources.Load<Texture2D>("Cursors/inventory_cursor");
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.AddItem(item);
        if (wasPickedUp)
        {
            GameObject.Find("Drunk Dancing Guy").GetComponent<Animator>().SetBool("Sad", true);
            Destroy(gameObject);
        }
    }
}
