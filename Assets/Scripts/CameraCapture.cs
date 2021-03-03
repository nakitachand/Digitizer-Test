using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    [SerializeField]
    private Camera ARCamera;

    public int fileCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Capture()
    {
        RenderTexture renderTexture = RenderTexture.active;
        RenderTexture.active = ARCamera.targetTexture;
        ARCamera.Render();
        Texture2D image = new Texture2D(ARCamera.targetTexture.width, ARCamera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, ARCamera.targetTexture.width, ARCamera.targetTexture.height), 0, 0);
        image.Apply();

        byte[] bytes = image.EncodeToPNG();
    }
}
