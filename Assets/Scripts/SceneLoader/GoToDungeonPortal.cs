using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDungeonPortal : Interactable
{
    [SerializeField]
    private GameObject portalPanel;

    public override void Interact()
    {
        base.Interact();

        player.GetComponent<MyCharacterController>().DisableMovement();
        portalPanel.SetActive(true);
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/portal_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }

    }
}
