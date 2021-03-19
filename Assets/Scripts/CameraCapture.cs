using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class CameraCapture : MonoBehaviour
{
    [SerializeField]
    private Camera ARCamera;

    public int fileCounter;

    private string currentDate;

    [SerializeField]
    private GameObject canvasRenderer;

    public void CaptureScreen()
    {
        StartCoroutine(Capture());
    }

    public IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture renderTexture = RenderTexture.active;
        RenderTexture.active = ARCamera.targetTexture;
        ARCamera.Render();
        Texture2D image = new Texture2D(ARCamera.targetTexture.width, ARCamera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, ARCamera.targetTexture.width, ARCamera.targetTexture.height), 0, 0);
        image.Apply();

        byte[] bytes = image.EncodeToPNG();

        //Save bytes to SaveData
        SaveData currentData = new SaveData();
        currentData.image = image;
        currentData.bytes = bytes;

        SaveImage(currentData);
        string storagePath = Application.persistentDataPath;
        string date = DateTime.Now.ToString("yyyy-dd-MM-hh-mm-ss");
        currentDate = date;
        File.WriteAllBytes(storagePath + $"/Digitizer/Trace Drawing {date}", bytes);
    }

    public void SaveImage(SaveData saveData)
    {
        string dataAsString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("Trace Drawing", dataAsString);
        PlayerPrefs.Save();
    }

    public void LoadImage(SaveData saveData)
    {
        Texture2D loadImage = new Texture2D(ARCamera.targetTexture.width, ARCamera.targetTexture.height);
        if(File.Exists(Application.persistentDataPath+$"/Digitizer/Trace Drawing {currentDate}"))
        {
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + $"/Digitizer/Trace Drawing {currentDate}");
            loadImage.LoadImage(bytes);
            canvasRenderer.GetComponent<Renderer>().material.mainTexture = loadImage;
            //might be getcomponent<image>
        }

        if(PlayerPrefs.HasKey("Trace Drawing"))
        {
            //File.
        }
    }

}
