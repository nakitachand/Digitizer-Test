﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This Script is used to raise Unity Events in reponse to pointer events
/// 
/// Heavily inspired by MRTK PointerHandler
/// </summary>
public class PointerHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    [Tooltip("Whether input events should be marked as used after handling so other handlers in the same game object ignore them")]
    //private bool markEventsAsUsed = false;
    /// <summary>
    /// Unity event raised on pointer down.
    /// </summary>
    public PointerUnityEvent OnPointerDown = new PointerUnityEvent();
    /// <summary>
    /// Unity event raised on pointer up.
    /// </summary>
    public PointerUnityEvent OnPointerUp = new PointerUnityEvent();
    /// <summary>
    /// Unity event raised on pointer clicked.
    /// </summary>
    public PointerUnityEvent OnPointerClicked = new PointerUnityEvent();
    /// <summary>
    /// Unity event raised every frame the pointer is down.
    /// </summary>
    public PointerUnityEvent OnPointerDragged = new PointerUnityEvent();
    //void IARPointerHandler.OnPointerDown(PointerEventData eventData)
    //{
    //    DebugManager.Instance.LogInfo($"Pointer down {eventData}");
    //    if (!eventData.used)
    //    {
    //        OnPointerDown.Invoke(eventData);
    //        if (markEventsAsUsed)
    //        {
    //            eventData.Use();
    //        }
    //    }
    //}
    //void IARPointerHandler.OnPointerUp(PointerEventData eventData)
    //{
    //    DebugManager.Instance.LogInfo($"Pointer up {eventData}");
    //    if (!eventData.used)
    //    {
    //        OnPointerUp.Invoke(eventData);
    //        if (markEventsAsUsed)
    //        {
    //            eventData.Use();
    //        }
    //    }
    //}
    //void IARPointerHandler.OnPointerClicked(PointerEventData eventData)
    //{
    //    DebugManager.Instance.LogInfo($"Pointer clicked {eventData}");
    //    if (!eventData.used)
    //    {
    //        OnPointerClicked.Invoke(eventData);
    //        if (markEventsAsUsed)
    //        {
    //            eventData.Use();
    //        }
    //    }
    //}
    //void IARPointerHandler.OnPointerDragged(PointerEventData eventData)
    //{
    //    DebugManager.Instance.LogInfo($"Pointer dragged {eventData}");
    //    if (!eventData.used)
    //    {
    //        OnPointerDragged.Invoke(eventData);
    //        if (markEventsAsUsed)
    //        {
    //            eventData.Use();
    //        }
    //    }
    //}
    /// <summary>
    /// For this to fire, must have a PhysicsRaycaster component
    /// on the camera
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClicked?.Invoke(eventData);
        //DebugManager.Instance.LogInfo($"On pointer clicked");
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
        OnPointerDown?.Invoke(eventData);
        //DebugManager.Instance.LogInfo($"On pointer down");
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
        OnPointerUp?.Invoke(eventData);
        //DebugManager.Instance.LogInfo($"On pointer up");
    }
    //public override void Process()
    //{
    //}
}