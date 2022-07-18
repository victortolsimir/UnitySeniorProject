using UnityEngine;

public class KeyInteract : Interactable
{
    [SerializeField]

    public UniqueID uniqueID;
    public ExclusionList exclusionList;
    public Item item;

    private void Start()
    {
        if (exclusionList.exclusions.Contains(uniqueID))
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
        bool wasPickedUp = Inventory.instance.AddItem(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        exclusionList.Add(uniqueID);
        gameObject.SetActive(false);
    }
}
