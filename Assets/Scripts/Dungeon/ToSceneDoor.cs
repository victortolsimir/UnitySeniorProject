using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSceneDoor : Interactable
{
    [SerializeField]

    private bool opened;
    private GameObject doorLeft;
    private GameObject doorRight;

    private bool rotating;

    public string sceneTarget = "Dungeon_Floor_5";

    private void Start()
    {
        doorLeft = gameObject.transform.GetChild(0).gameObject;
        doorRight = gameObject.transform.GetChild(1).gameObject;
    }

    public override void ChangeCursor()
    {
        Transform player = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance <= this.radius)
        {
            var cursor = Resources.Load<Texture2D>("Cursors/door_cursor");
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public override void Interact()
    {
        StartCoroutine(Open());
        StartCoroutine(GoToDoor());
    }

    private void GoToNextFloor()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneLoader.LoadNextFloor();
        SceneLoader.Load(sceneTarget);
    }

    private IEnumerator GoToDoor()
    {
        Transform player = GameObject.Find("Player").transform;
        Transform camera = GameObject.Find("Main Camera").transform;
        Vector3 startPos = player.position;
        Vector3 endPos = gameObject.transform.position;

        GameObject.Find("Player").GetComponent<MyCharacterController>().enabled = false;
        GameObject.Find("Character_NoneEquip").GetComponent<Animator>().SetFloat("Forward", 5.0f, 0.1f, Time.deltaTime);

        player.LookAt(gameObject.transform);

        Quaternion playerRotation = player.transform.rotation;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            //player.transform.position = Vector3.Lerp(startPos, endPos, t / 1f);
            player.transform.rotation = Quaternion.Slerp(playerRotation, gameObject.transform.rotation, t / 1f);
            player.transform.position = Vector3.Lerp(startPos, endPos, t / 1f);
            yield return null;
        }

        startPos = endPos;
        endPos = startPos + (gameObject.transform.rotation * new Vector3(0, 0, 4));
        GameObject.Find("Main Camera").GetComponent<RealCamera>().enabled = false;
        GameObject cam = GameObject.Find("Main Camera");
        Vector3 pos = cam.transform.position;
        Vector3 offset = cam.transform.position - player.position;
        player.transform.rotation = gameObject.transform.rotation;


        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            player.transform.position = Vector3.Lerp(startPos, endPos, t / 1f);
            //cam.transform.position = new Vector3(pos.x, player.transform.position.y + offset.y, pos.z);
            yield return null;
        }

        //GameObject.Find("Player").GetComponent<MyCharacterController>().enabled = true;
        //GameObject.Find("Main Camera").GetComponent<RealCamera>().enabled = true;

        GoToNextFloor();
    }

    private IEnumerator Open()
    {
        rotating = true;
        Quaternion startRotation = doorLeft.transform.rotation;
        Quaternion startRotation2 = doorRight.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(0, -90, 0)) * startRotation;
        Quaternion endRotation2 = Quaternion.Euler(new Vector3(0, 90, 0)) * startRotation2;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            doorLeft.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / 0.5f);
            doorRight.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t / 0.5f);
            yield return null;
        }
        doorLeft.transform.rotation = endRotation;
        doorRight.transform.rotation = endRotation2;

        startRotation = doorLeft.transform.rotation;
        startRotation2 = doorRight.transform.rotation;
        endRotation = Quaternion.Euler(new Vector3(0, 90, 0)) * startRotation;
        endRotation2 = Quaternion.Euler(new Vector3(0, -90, 0)) * startRotation2;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            doorLeft.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / 1);
            doorRight.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t / 1);
            yield return null;
        }
        doorLeft.transform.rotation = endRotation;
        doorRight.transform.rotation = endRotation2;
        rotating = false;
    }


}
