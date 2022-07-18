using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloorTrigger : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        GoToNextFloor();
    }

    private void GoToNextFloor()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneLoader.LoadNextFloor();
    }
}
