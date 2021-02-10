using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class PlaneSelectionManager : MonoBehaviour
{
    [SerializeField]
    private Camera arCamera;
    private Vector2 touchPosition = default;
    private Transform selection;
    private ISelectionResponse selectionResponse;
    void Awake()
    {
        selectionResponse = GetComponent<SelectionResponse>();
    }
    void Update()
    {
        // Deselection
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                if (selection != null)
                {
                    var _selection = selection;
                    selectionResponse.OnDeselect(_selection);
                }
            }
        }
        #region raycast
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    var _selection = hitObject.transform;
                    var selectedPlane = hitObject.transform.GetComponent<PlaneSelector>();
                    if (selectedPlane)
                    {
                        selection = _selection;
                        DrawManager.Instance.selectedPlane = selection;
                    }
                }
            }
        }
        #endregion
        // Selection
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                if (selection != null)
                {
                    var _selection = selection;
                    selectionResponse.OnSelect(_selection);
                }
            }
        }
    }
}