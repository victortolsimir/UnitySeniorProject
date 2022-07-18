using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        //pick up the item
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
        if(wasPickedUp)
        {
           Destroy(gameObject);
        }
        
    }
}
