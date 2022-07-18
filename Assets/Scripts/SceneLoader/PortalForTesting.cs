using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalForTesting : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        SceneManager.LoadScene("Dungeon_Floor_1");
    }
}
