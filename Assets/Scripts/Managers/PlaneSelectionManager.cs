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
    private ARAnchorManager anchorManager;
    private List<ARAnchor> aRAnchors = new List<ARAnchor>();

    public static PlaneSelectionManager Instance;
        

    void Awake()
    {
        selectionResponse = GetComponent<SelectionResponse>();

        if (Instance == null)
        { 
            Instance = this; 
        }
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
                    selection.SetParent(null);
                    aRAnchors.Remove(aRAnchors[aRAnchors.Count]);
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
                    ARAnchor anchor = anchorManager.AddAnchor(new Pose(_selection.position, _selection.rotation));
                    aRAnchors.Add(anchor);
                    selection.parent = anchor.transform;
                }
            }
        }
    }
}