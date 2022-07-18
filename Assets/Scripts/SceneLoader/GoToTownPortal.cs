using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTownPortal : Interactable
{
    [SerializeField]
    private UniqueID spawnID;

    public override void Interact()
    {
        base.Interact();
        GoToTown();
    }

    private void GoToTown()
    {
        // GlobalControl.instance.IsTeleporting = true;
        GlobalControl.instance.spawnID = spawnID;
        SceneLoader.Load("Town");
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
