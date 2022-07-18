using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteractable : Interactable
{
    //Character player;
    public override void Interact()
    {
        //base.Interact();
        GlobalControl.instance.playerPosition = player.position;
        GlobalControl.instance.Save();
    }


    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/save_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

}
