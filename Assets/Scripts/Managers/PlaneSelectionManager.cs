using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;



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

    [SerializeField]
    private GameObject planeButton;

    [SerializeField]
    private Text planeButtonText;

    [SerializeField]
    private Text lockedButtonText;

    [SerializeField]
    private ARPlaneManager aRPlaneManager;

    private bool CanSelect
    {
        get;
        set;
    }

    private bool IsShown
    {
        get;
        set;
    }

    private bool IsLocked
    {
        get;
        set;
    }

    void Awake()
    {
        selectionResponse = GetComponent<SelectionResponse>();

        aRPlaneManager = GetComponent<ARPlaneManager>();

        if (CanSelect) { planeButtonText.text = "Hide Planes"; }
        else { planeButtonText.text = "Show Planes"; }
    }

    public void Update()
    {
        if (CanSelect) { return; }

        PlaneSelectionSequence();

    }

    public void AllowSelect()
    {
        CanSelect = !CanSelect;
    }

    //public void AllowSelect(bool value)
    //{
    //    CanSelect = value;
    //}

    public void PlaneSelectionSequence()
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
                    //DebugManager.Instance.LogInfo($"OnDeselection invoked.");
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

                if (Physics.Raycast(ray, out RaycastHit hitObject))
                {
                    var _selection = hitObject.transform;
                    var selectedTransform = hitObject.transform.GetComponent<PlaneSelector>();
                    selectedPlane = hitObject.transform.GetComponent<ARPlane>();
                    if (selectedPlane && selectedTransform)
                    {
                        selection = _selection;
                        DrawManager.Instance.selectedPlane = selection;
                        //DebugManager.Instance.LogInfo($"selectedPlane = selection.");
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
                    //DebugManager.Instance.LogInfo($"OnSelection invoked.");
                    anchorManager.AttachAnchor(selectedPlane, new Pose(_selection.position, _selection.rotation));
                    aRAnchors.Add(selectedPlane.GetComponent<ARAnchor>());

                }
            }
        }
    }

    public void LockPlaneButton()
    {
        //keep selected plane fixed and visible
        //hide plane prefab after 2 seconds
        //display text prompt to begin drawing
        DebugManager.Instance.LogInfo($"Lock Planes.");

        if (!IsLocked) //change this variable to islocked bool
        {
            lockedButtonText.GetComponentInChildren<Text>().text = "Lock Plane";
            DebugManager.Instance.LogInfo($"Locked = {IsLocked}");
            //Lock Plane function;
        }
        else
        {
            lockedButtonText.GetComponentInChildren<Text>().text = "Unlock Plane";
            DebugManager.Instance.LogInfo($"Locked = {IsLocked}");
            //Unlock Plane function;
        }

        IsShown = !IsShown;
    }

    public void HidePlanes()
    {
        //if CanSelect == true
        //Text = "Hide Planes"
        //else 
        //Text = "Show Planes"
        DebugManager.Instance.LogInfo($"Can Select = {CanSelect}");

        IsShown = true;

        if (IsShown)
        {
            planeButtonText.GetComponentInChildren<Text>().text = "Hide Planes";
            DebugManager.Instance.LogInfo($"Can Select = {IsShown}");
            SetTrackablesActive(!IsShown);
        }
        else
        {
            planeButtonText.GetComponentInChildren<Text>().text = "Show Planes";
            DebugManager.Instance.LogInfo($"Can Select = {IsShown}");
            SetTrackablesActive(IsShown);
        }

        
        CanSelect = !CanSelect;
        //aRPlaneManager.SetTrackablesActive(CanSelect);
        
    }

    public void SetTrackablesActive(bool active)
    {
        aRPlaneManager.SetTrackablesActive(!active);
    }
}