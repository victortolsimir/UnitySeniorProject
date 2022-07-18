using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControllerDescend : Interactable
{
    [SerializeField]

    public GameObject elevator;

    public List<UniqueID> requiredCompletedList;
    public ExclusionList exclusionList;

    public float distanceMultiplier = 1;
    public float speedMultiplier = 1;

    public GameObject bridge1;
    public GameObject bridge2;

    private bool opened;
    private bool moving;
    private bool descending = true;
    private bool lockAnimation;

    public bool movedlastFloor;

    private Vector3 startingPos;

    private Coroutine movement;

    private void Start()
    {
        startingPos = elevator.transform.position;
        if (GlobalControl.instance.cameFromNextFloor)
        {
            MovedToPrev();
        }
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius && !opened)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/gears_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public override void Interact()
    {
        int missingItems = requiredCompletedList.Count;

        foreach (UniqueID uID in requiredCompletedList)
        {
            if (exclusionList.exclusions.Contains(uID))
            {
                missingItems--;
            }
        }
        // If there are no keys missing
        if (missingItems == 0)
        {
            if (!moving)
            {
                movement = StartCoroutine(Move(descending));
                descending = !descending;

                //opened = true;

                //exclusionList.Add(uniqueDoorID);
            }
            else
            {
                if (!lockAnimation)
                {
                    StopCoroutine(movement);
                    moving = false;
                }
            }
        }
        else
        {
            if (missingItems == 1)
            {
                MessagePanelSystem.ShowMessage("You are missing " + missingItems + " key.");
            }
            else
            {
                MessagePanelSystem.ShowMessage("You are missing " + missingItems + " keys.");
            }
        }
    }

    public void MovedToPrev()
    {
        movedlastFloor = true;
        elevator.transform.position = startingPos + new Vector3(0, -20 * distanceMultiplier, 0);

        GameObject.Find("Player").transform.position = gameObject.transform.position + new Vector3(0, 0, -2);

        bridge1.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0)) * bridge1.transform.rotation;
        bridge2.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)) * bridge2.transform.rotation;

        descending = false;
        movement = StartCoroutine(Move(descending));
        descending = !descending;
    }

    private IEnumerator Move(bool descending)
    {
        moving = true;

        if (elevator.transform.position == startingPos)
        {
            lockAnimation = true;
            Quaternion startRotation1 = bridge1.transform.rotation;
            Quaternion startRotation2 = bridge2.transform.rotation;
            Quaternion endRotation1 = Quaternion.Euler(new Vector3(-90, 0, 0)) * startRotation1;
            Quaternion endRotation2 = Quaternion.Euler(new Vector3(90, 0, 0)) * startRotation2;

            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                bridge1.transform.rotation = Quaternion.Slerp(startRotation1, endRotation1, t / 1);
                bridge2.transform.rotation = Quaternion.Slerp(startRotation2, endRotation2, t / 1);
                yield return null;
            }
            bridge1.transform.rotation = endRotation1;
            bridge2.transform.rotation = endRotation2;

            lockAnimation = false;
        }


        float distance;

        Vector3 startPosition = elevator.transform.position;
        Vector3 endPosition;

        if (descending)
        {
            endPosition = startingPos + new Vector3(0, -20 * distanceMultiplier, 0);
        }
        else
        {
            endPosition = startingPos;
        }
        distance = Vector3.Distance(startPosition, endPosition) * speedMultiplier;

        if (distance < 0)
        {
            distance = -distance;
        }


        for (float t = 0; t < distance; t += Time.deltaTime)
        {
            elevator.transform.position = Vector3.Lerp(startPosition, endPosition, t / distance);
            yield return null;
        }

        elevator.transform.position = endPosition;

        if (elevator.transform.position == startingPos)
        {
            lockAnimation = true;
            Quaternion startRotation1 = bridge1.transform.rotation;
            Quaternion startRotation2 = bridge2.transform.rotation;
            Quaternion endRotation1 = Quaternion.Euler(new Vector3(90, 0, 0)) * startRotation1;
            Quaternion endRotation2 = Quaternion.Euler(new Vector3(-90, 0, 0)) * startRotation2;

            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                bridge1.transform.rotation = Quaternion.Slerp(startRotation1, endRotation1, t / 1);
                bridge2.transform.rotation = Quaternion.Slerp(startRotation2, endRotation2, t / 1);
                yield return null;
            }
            bridge1.transform.rotation = endRotation1;
            bridge2.transform.rotation = endRotation2;

            lockAnimation = false;
        }
        else if (elevator.transform.position == startingPos + new Vector3(0, -20 * distanceMultiplier, 0))
        {
            GlobalControl.instance.cameFromNextFloor = false;
            SceneLoader.LoadNextFloor();
        }

        moving = false;

        //MessagePanelSystem.ShowMessage("Arrived.");
    }
}
