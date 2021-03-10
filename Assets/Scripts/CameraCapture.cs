using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class CameraCapture : MonoBehaviour
{
    [SerializeField]
    private Camera ARCamera;

    public int fileCounter;
    
    

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

        // Create a Web Form
        WWWForm form = new WWWForm();
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("fileUpload", bytes);
        // Upload to a cgi script
        var w = UnityWebRequest.Post("http://localhost/cgi-bin/env.cgi?post", form);
        yield return w.SendWebRequest();
        if (w.isNetworkError || w.isHttpError)
        {
            Debug.Log(w.error);
        }
        else
        {
            Debug.Log("Finished Uploading Screenshot");
        }
    }

    public void SaveImage(SaveData saveData)
    {

    }

}
