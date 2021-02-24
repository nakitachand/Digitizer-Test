using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrawButtonDown : MonoBehaviour
{
    bool _pressed = false;

    public void Update()
    {
        if(!_pressed)
        {
            return;
        }

        DrawManager.Instance.DrawOnTouch();
    }

    public void OnPointerDown()
    {
        _pressed = true;
    }

    public void OnPointerUp()
    {
        _pressed = false;
    }
}
