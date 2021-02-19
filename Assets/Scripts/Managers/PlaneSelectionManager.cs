using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using UnityEngine.Events;
public class PlaneSelectionManager : Singleton<PlaneSelectionManager>
{
    [SerializeField]
    private Camera arCamera;
    private Vector2 touchPosition = default;
    private Transform selection;
    private ISelectionResponse selectionResponse;
    private ARAnchorManager anchorManager;
    private List<ARAnchor> aRAnchors = new List<ARAnchor>();
    private ARPlane selectedPlane;
    public UnityEvent OnSelection;
    public UnityEvent OnDeselection;


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
                    OnDeselection?.Invoke();
                    if (aRAnchors.Any<ARAnchor>())
                    {
                        aRAnchors.RemoveAt(aRAnchors.Count - 1);
                    }
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
                    var selectedTransform = hitObject.transform.GetComponent<PlaneSelector>();
                    selectedPlane = hitObject.transform.GetComponent<ARPlane>();
                    if (selectedPlane && selectedTransform)
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
                    OnSelection?.Invoke();
                    anchorManager.AttachAnchor(selectedPlane, new Pose(_selection.position, _selection.rotation));
                    aRAnchors.Add(selectedPlane.GetComponent<ARAnchor>());
                    
                }
            }
        }
    }
}