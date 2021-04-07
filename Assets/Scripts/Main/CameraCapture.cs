using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class CameraCapture : MonoBehaviour
{
    [SerializeField]
    private Camera ARCamera;

    //public int fileCounter;

    private string currentDate;

    [SerializeField]
    private GameObject canvasRenderer;

    public RenderTexture renderTexture;

    public ARCameraBackground aRCameraBackground;

    private void Start()
    {
        #if PLATFORM_ANDROID
        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) && !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        #endif
    }

    public void CaptureScreen()
    {
        StartCoroutine(Capture());
    }


    public IEnumerator Capture()
    {
        yield return new WaitForEndOfFrame();
        DebugManager.Instance.LogInfo("Capture Started");
        renderTexture = RenderTexture.active;
        Graphics.Blit(null, renderTexture, aRCameraBackground.material);
        DebugManager.Instance.LogInfo($"{renderTexture}");
        //RenderTexture.active = ARCamera.targetTexture;
        ARCamera.Render();
        Texture2D image = new Texture2D(ARCamera.targetTexture.width, ARCamera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, ARCamera.targetTexture.width, ARCamera.targetTexture.height), 0, 0);
        image.Apply();
        DebugManager.Instance.LogInfo("image.Apply");

        byte[] bytes = image.EncodeToPNG();

        //Save bytes to SaveData
        SaveData currentData = new SaveData();
        currentData.image = image;
        currentData.bytes = bytes;

        SaveImage(currentData);

        ///storage/emulated/0/Android/data/<packagename>/files
        //string storagePath = Application.persistentDataPath;
        string storagePath = "/Android/data/com.Creative254.DigitizerTest/files";
        string date = DateTime.Now.ToString("yyyy-dd-MM-hh-mm-ss");
        currentDate = date;
        if(!Directory.Exists($"{storagePath} + /Digitizer/TraceDrawing"))
        {
            Directory.CreateDirectory($"{storagePath} + /Digitizer/TraceDrawing");
            File.WriteAllBytes(storagePath + $"/Digitizer/TraceDrawing {date}", bytes);
        }
        File.WriteAllBytes(storagePath + $"/Digitizer/TraceDrawing {date}", bytes);

        DebugManager.Instance.LogInfo(storagePath + $"/Digitizer/TraceDrawing {date}");
    }

    public void SaveImage(SaveData saveData)
    {
        string dataAsString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("Trace Drawing", dataAsString);
        PlayerPrefs.Save();

        DebugManager.Instance.LogInfo($"Saved image to Player Prefs");
    }

    public void LoadImagePlayerPerfs()
    {
        if(PlayerPrefs.HasKey("Trace Drawing"))
        {
            string dataAsString = PlayerPrefs.GetString("Trace Drawing");
            JsonUtility.FromJson(dataAsString, typeof(SaveData));
        }
    }

    public void LoadImage()
    {
        Texture2D loadImage = new Texture2D(ARCamera.targetTexture.width, ARCamera.targetTexture.height);
        if(File.Exists(Application.persistentDataPath+$"/Digitizer/Trace Drawing {currentDate}"))
        {
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + $"/Digitizer/Trace Drawing {currentDate}");
            loadImage.LoadImage(bytes);
            canvasRenderer.GetComponent<Image>().material.mainTexture = loadImage;
            //might be getcomponent<image>
        }

        if(PlayerPrefs.HasKey("Trace Drawing"))
        {
            //File.
        }
    }

}
