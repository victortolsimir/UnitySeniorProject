using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotTest : MonoBehaviour
{
    public Image screenshot;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath+ "/Junk For Testing/TestScreenShot.png");
            Debug.Log(Application.dataPath + "/Junk For Testing/TestScreenShot.png");
            DisplayImage(Application.dataPath + "/Junk For Testing/TestScreenShot.png");
        }

    }

    private void DisplayImage(string filePath)
    {
        Texture2D texture = LoadPNG(filePath);
        if(texture != null)
        {
            Sprite pathImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            screenshot.sprite = pathImage;
        }
        
    }

    private static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
