using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarredDoorInteract : Interactable
{
    [SerializeField]

    public UniqueID uniqueDoorID;
    public ExclusionList exclusionList;

    private bool opened;
    private GameObject doorLeft;
    private GameObject doorRight;
    private GameObject block;

    private bool rotating;

    private void Start()
    {
        doorLeft = gameObject.transform.GetChild(0).gameObject;
        doorRight = gameObject.transform.GetChild(1).gameObject;
        block = gameObject.transform.GetChild(2).gameObject;

        if (exclusionList.exclusions.Contains(uniqueDoorID))
        {
            opened = true;
        }
        if (opened)
        {
            doorLeft.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0)) * doorLeft.transform.rotation;
            doorRight.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)) * doorRight.transform.rotation;

            block.transform.position = block.transform.position + (gameObject.transform.rotation * new Vector3(0.2f, 0, 0));
            block.transform.position = block.transform.position + (gameObject.transform.rotation * new Vector3(0, -1.216f, 0));
            block.transform.rotation = Quaternion.Euler(gameObject.transform.rotation * new Vector3(0, 0, -90)) * block.transform.rotation;
        }
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius && !opened)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/door_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public override void Interact()
    {
        if (!opened)
        {
            Vector3 positionDifference = (GameObject.Find("Player").transform.position - gameObject.transform.position).normalized;
            if (Vector3.Dot(positionDifference, transform.forward) > 0)
            {
                if (!rotating)
                {
                    StartCoroutine(Rotate(new Vector3(0, -90, 0), (float)0.5));

                    opened = true;

                    exclusionList.Add(uniqueDoorID);
                }
            }
            else
            {
                MessagePanelSystem.ShowMessage("Open from the other side.");
            }
        }
    }

    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;

        Quaternion startRotationBlock = block.transform.rotation;
        Quaternion endRotationBlock = Quaternion.Euler(gameObject.transform.rotation * new Vector3(0, 0, -90)) * startRotationBlock;
        Vector3 startPositionBlock = block.transform.position;
        Vector3 endPositionBlock = block.transform.position + (gameObject.transform.rotation * new Vector3(0.2f, 0, 0));

        for (float t = 0; t < 0.3; t += Time.deltaTime)
        {
            block.transform.position = Vector3.Lerp(startPositionBlock, endPositionBlock, t / 0.3f);
            yield return null;
        }

        block.transform.position = endPositionBlock;

        startPositionBlock = block.transform.position;
        endPositionBlock = block.transform.position + (gameObject.transform.rotation * new Vector3(0, -1.216f, 0));

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            block.transform.position = Vector3.Lerp(startPositionBlock, endPositionBlock, t / 1);
            block.transform.rotation = Quaternion.Lerp(startRotationBlock, endRotationBlock, t / 1);
            yield return null;
        }

        block.transform.position = endPositionBlock;
        block.transform.rotation = endRotationBlock;

        // Opens doors

        Quaternion startRotationLeft = doorLeft.transform.rotation;
        Quaternion startRotationRight = doorRight.transform.rotation;
        Quaternion endRotationLeft = Quaternion.Euler(angles) * startRotationLeft;
        Quaternion endRotationRight = Quaternion.Euler(new Vector3(0, 90, 0)) * startRotationRight;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            doorLeft.transform.rotation = Quaternion.Lerp(startRotationLeft, endRotationLeft, t / duration);
            doorRight.transform.rotation = Quaternion.Lerp(startRotationRight, endRotationRight, t / duration);
            yield return null;
        }
        doorLeft.transform.rotation = endRotationLeft;
        doorRight.transform.rotation = endRotationRight;
        rotating = false;
    }
}
