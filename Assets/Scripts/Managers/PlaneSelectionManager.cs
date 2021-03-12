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
    private Transform lastSelectedObject;
    private ISelectionResponse selectionResponse;
    private ARAnchorManager anchorManager;
    private List<ARAnchor> aRAnchors = new List<ARAnchor>();
    private ARPlane selectedPlane;

    public UnityEvent OnSelection;
    public UnityEvent OnDeselection;

    [SerializeField]
    private Button planeButton;

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

    private bool isShown = false;

    public bool usingPointerHandler = false;
    

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

    private bool IsLocked
    {
        get;
        set;
    }

    void Awake()
    {
        selectionResponse = GetComponent<SelectionResponse>();
    }

    public void OnEnable()
    {
        planeButton.onClick.AddListener(TogglePlanes);
        planeButton.onClick.AddListener(ToggleText);
    }

    public void OnDisable()
    {
        planeButton.onClick.RemoveListener(TogglePlanes);
        planeButton.onClick.RemoveListener(ToggleText);
    }

    public void Update()
    {
        if (CanSelect) { return; }
        if (usingPointerHandler) { return; }

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
                HandleDeselection(lastSelectedObject);
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
                        lastSelectedObject = _selection;
                        DrawManager.Instance.selectedPlane = lastSelectedObject;
                        
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
                HandleSelection(lastSelectedObject);
            }
        }
    }

    public void TogglePlanes() //add arg: bool value
    {
        //value = IsShown
        
        isShown = !isShown; //replace with value = !value

        if (IsShown) 
        {
            aRPlaneManager.SetTrackablesActive(isShown); 
        }
        else 
        {
            aRPlaneManager.SetTrackablesActive(isShown); 
        }

    }

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

    public void HandleSelection(Transform selected)
    {
        if (lastSelectedObject != null)
        {
            var _selection = lastSelectedObject;
            selectionResponse.OnSelect(_selection);
            OnSelection?.Invoke();
            anchorManager.AttachAnchor(selectedPlane, new Pose(_selection.position, _selection.rotation));
            aRAnchors.Add(selectedPlane.GetComponent<ARAnchor>());

        }
    }

    public void HandleDeselection(Transform deselected)
    {
        if (lastSelectedObject != null)
        {
            var _selection = lastSelectedObject;
            selectionResponse.OnDeselect(_selection);
            OnDeselection?.Invoke();
            if (aRAnchors.Any<ARAnchor>())
            {
                aRAnchors.RemoveAt(aRAnchors.Count - 1);
            }
        }
    }

}