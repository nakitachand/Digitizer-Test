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
    
    public UnityEvent OnSelection;
    public UnityEvent OnDeselection;

    [SerializeField]
    private Button planeButton;

    [SerializeField]
    private Text planeButtonText;


    [SerializeField]
    private ARPlaneManager aRPlaneManager;


    //private Vector2 touchPosition = default;
    //private Transform lastSelectedObject;
    //private ISelectionResponse selectionResponse;
    //private ARAnchorManager anchorManager;
    //private List<ARAnchor> aRAnchors = new List<ARAnchor>();
    //private ARPlane selectedPlane;

    private bool CanSelect
    {
        get;
        set;
    }


    private bool isShown = false;

    //bool property to get/set isShown value above for showing/hiding planes
    public bool IsShown
    {
        get 
        {
            return this.isShown;
        }

        set
        {
            isShown = value;
        }
    }

    public bool usingPointerHandler = false;

    //void Awake()
    //{
    //    selectionResponse = GetComponent<SelectionResponse>();
    //}



    public void Update()
    {
        if (CanSelect) { return; }
        if (usingPointerHandler) { return; }

        //PlaneSelectionSequence();
    }

    public void AllowSelect()
    {
        CanSelect = !CanSelect;
    }

    public void OnEnable() //for plane visibility button
    {
        planeButton.onClick.AddListener(TogglePlanes);
        planeButton.onClick.AddListener(ToggleText);
    }

    public void OnDisable() //for plane visibility button
    {
        planeButton.onClick.RemoveListener(TogglePlanes);
        planeButton.onClick.RemoveListener(ToggleText);
    }

    //function linked to UI button to toggle plane visibility
    public void TogglePlanes() 
    {
        
        isShown = !isShown; 

        if (IsShown) 
        {
            aRPlaneManager.SetTrackablesActive(isShown); 
        }
        else 
        {
            aRPlaneManager.SetTrackablesActive(isShown); 
        }

    }


    //function linked to UI button text to update visibility action type when pressed
    public void ToggleText()
    {
        if(isShown)
        {
            planeButtonText.GetComponentInChildren<Text>().text = "Hide Planes";
        }
        else
        {
            planeButtonText.GetComponentInChildren<Text>().text = "Show Planes";
        }
    }


    //public void PlaneSelectionSequence()
    //{
    //    // Deselection
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            HandleDeselection(lastSelectedObject);

    //        }
    //    }

    //    #region raycast
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        touchPosition = touch.position;
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            Ray ray = arCamera.ScreenPointToRay(touchPosition);

    //            if (Physics.Raycast(ray, out RaycastHit hitObject))
    //            {
    //                var _selection = hitObject.transform;
    //                var selectedTransform = hitObject.transform.GetComponent<PlaneSelector>();
    //                selectedPlane = hitObject.transform.GetComponent<ARPlane>();
    //                if (selectedPlane && selectedTransform)
    //                {
    //                    lastSelectedObject = _selection;
    //                    DrawManager.Instance.selectedPlane = lastSelectedObject;

    //                }
    //            }
    //        }
    //    }
    //    #endregion

    //    // Selection
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        touchPosition = touch.position;
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            HandleSelection(lastSelectedObject);

    //        }
    //    }
    //}

    //public void HandleSelection(Transform selected)
    //{
    //    if (lastSelectedObject != null)
    //    {
    //        var _selection = lastSelectedObject;
    //        selectionResponse.OnSelect(_selection);
    //        OnSelection?.Invoke();
    //        anchorManager.AttachAnchor(selectedPlane, new Pose(_selection.position, _selection.rotation));
    //        aRAnchors.Add(selectedPlane.GetComponent<ARAnchor>());

    //    }
    //}

    //public void HandleDeselection(Transform deselected)
    //{
    //    if (lastSelectedObject != null)
    //    {
    //        var _selection = lastSelectedObject;
    //        selectionResponse.OnDeselect(_selection);
    //        OnDeselection?.Invoke();
    //        if (aRAnchors.Any<ARAnchor>())
    //        {
    //            aRAnchors.RemoveAt(aRAnchors.Count - 1);
    //        }
    //    }
    //}




}