using UnityEngine;

public class ClimbLadder : Interactable
{
    private GameObject playerObj;
    private GameObject mainCamera;
    public Vector3 bottomPos;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/ladder_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public override void Interact()
    {

        if (playerObj.transform.position.y < 4)
        {
            playerObj.transform.position = new Vector3(bottomPos.x, bottomPos.y + 4, bottomPos.z + 6);
        }
        else
        {
            playerObj.transform.position = bottomPos;
        }
        if (mainCamera != null)
        {
            mainCamera.transform.position = playerObj.transform.position;
        }
    }
}
