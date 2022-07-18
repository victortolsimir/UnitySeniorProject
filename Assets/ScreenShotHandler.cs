using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotHandler : MonoBehaviour
{
    public static ScreenShotHandler instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void TakeGameSaveShot()
    {
        string path = $"{GlobalControl.instance.SavePath}{GlobalControl.instance.playerName}.png";
        ScreenCapture.CaptureScreenshot(path);
        Debug.Log($"Screenshot taken and saved at {path}.");
    }
}
