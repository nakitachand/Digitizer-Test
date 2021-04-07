using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    bool takePicture;

    [SerializeField]
    MediaCapture mediaCapture;

    //gets called at the end of every render frame
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(takePicture)
        {
            takePicture = false;

            var tempRend = RenderTexture.GetTemporary(source.width, source.height);
            Graphics.Blit(source, tempRend);

            Texture2D tempTexture = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            Rect rect = new Rect(0, 0, source.width, source.height);
            tempTexture.ReadPixels(rect, 0, 0, false);
            tempTexture.Apply();
            NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(tempTexture, "Digitizer Test", "Image.png", (success, path) => DebugManager.Instance.LogInfo("Saved: " + success + " " + path));
            mediaCapture.OpenScreen(tempTexture);
            RenderTexture.ReleaseTemporary(tempRend);

        }
        
        Graphics.Blit(source, destination);

    }

    public void TakeScreenShot()
    {
        takePicture = true;
    }
}
