using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    //for use later
    public string[] dialogue;
    public string theName;

    public override void Interact()
    {
        base.Interact();
        Greet();
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

    private void Greet()
    {
        int randomIndex = Random.Range(0, dialogue.Length);
        MessagePanelSystem.ShowMessage("Hiya stranger!I am " + this.theName + " nice to meet you!");

        StartCoroutine(BeginDialogue(randomIndex));
    }

    private IEnumerator BeginDialogue(int randomIndex)
    {
        yield return new WaitForSeconds(1.5f);

        MessagePanelSystem.ShowMessage(dialogue[randomIndex]);
    }
}
