using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScreenUtils
{ 
    public static Vector2 GetScreenCenter()
    {
        float screenCenterX = Screen.width * 0.5f;
        float screenCenterY = Screen.height * 0.5f;

        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            return new Vector2(screenCenterY, screenCenterX);
        }

        return new Vector2(screenCenterX, screenCenterY);
    }

    
}


[System.Serializable]
public class PointerUnityEvent: UnityEvent<PointerEventData>
{

}
