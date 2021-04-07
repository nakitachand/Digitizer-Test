using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaCapture : ScreenBase
{
    [SerializeField]
    RawImage mediaImage;
    
    protected override void Start()
    {
        base.Start();
        SetScreen(false);
    }

    public void OpenScreen(Texture imageTex)
    {
        mediaImage.texture = imageTex;
        SetScreen(true);

    }

    public void CloseScreen()
    {
        SetScreen(false);
    }

}
