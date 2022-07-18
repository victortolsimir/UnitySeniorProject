using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrevFloorTrigger : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        GoToPrevFloor();
    }

    private void GoToPrevFloor()
    {
        GlobalControl.instance.movingToPrevFloor = true;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneLoader.LoadPreviousFloor();
    }
}
