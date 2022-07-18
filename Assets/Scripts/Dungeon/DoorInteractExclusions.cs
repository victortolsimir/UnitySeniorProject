using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractExclusions : Interactable
{
    [SerializeField]

    public string description = "";

    public UniqueID uniqueDoorID;
    public ExclusionList doorExclusionList;

    public List<UniqueID> keyList;
    public ExclusionList keyExclusionList;

    private bool opened;
    private GameObject doorLeft;
    private GameObject doorRight;

    private bool rotating;

    private void Start()
    {
        doorLeft = gameObject.transform.GetChild(0).gameObject;
        doorRight = gameObject.transform.GetChild(1).gameObject;

        if (doorExclusionList.exclusions.Contains(uniqueDoorID))
        {
            opened = true;
        }
        if (opened)
        {
            doorLeft.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0)) * doorLeft.transform.rotation;
            doorRight.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)) * doorRight.transform.rotation;
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

            // If there are no keys needed
            if (keyList.Count == 0)
            {
                StartCoroutine(Rotate(new Vector3(0, -90, 0), (float)0.5));

                opened = true;

                doorExclusionList.Add(uniqueDoorID);
            }
            // There are keys
            else
            {
                int missingKeys = keyList.Count;

                foreach (UniqueID curKey in keyList)
                {
                    if (keyExclusionList.exclusions.Contains(curKey))
                    {
                        missingKeys--;
                    }
                }
                // If there are no keys missing
                if (missingKeys == 0)
                {
                    if (!rotating)
                    {
                        StartCoroutine(Rotate(new Vector3(0, -90, 0), (float)0.5));

                        opened = true;

                        doorExclusionList.Add(uniqueDoorID);
                    }
                }
                else
                {
                    MessagePanelSystem.ShowMessage(description);
                }
            }
        }
    }

    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        rotating = true;
        Quaternion startRotation = doorLeft.transform.rotation;
        Quaternion startRotation2 = doorRight.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        Quaternion endRotation2 = Quaternion.Euler(new Vector3(0, 90, 0)) * startRotation2;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            doorLeft.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            doorRight.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t / duration);
            yield return null;
        }
        doorLeft.transform.rotation = endRotation;
        doorRight.transform.rotation = endRotation2;
        rotating = false;
    }


}
