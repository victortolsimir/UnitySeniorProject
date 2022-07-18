using UnityEngine;

public class MultLadderClimb : Interactable
{
    private GameObject playerObj;
    private GameObject mainCamera;
    public Vector3 playerTeleportPos;

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
        playerObj.transform.position = playerTeleportPos;
        if (mainCamera != null)
        {
            mainCamera.transform.position = playerTeleportPos;
        }
    }
}
