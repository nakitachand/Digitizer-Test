using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


//This script is attached to the ARPlane prefab to enable selection and deselection on click
//PointerHandler.cs implements onClick events for each instantiated prefab

public class PlaneSelector : MonoBehaviour
{
    private bool planeIsSelected;

    [SerializeField]
    private Material selectedMaterial;

    [SerializeField]
    private Material originalMaterial;

    public static GameObject selectedPlane = null;

    public GraphicRaycaster raycaster;

    public bool IsSelected
    {
        get
        {
            return this.planeIsSelected;
        }

        set
        {
            planeIsSelected = value;
        }
    }

    public void Awake()
    {
        selectedPlane = null;
    }

    //linked to onpointerclicked event in plane prefab
    public void HandleSelection()
    {
        PlaneSelector[] allPlanes = FindObjectsOfType<PlaneSelector>();

        foreach (PlaneSelector GO in allPlanes)
        {
            if (GO.IsSelected != selectedPlane)
            {
                GO.Deselect();
            }
            else
            {
                GO.Select();
                //AssignPlane(); //assigns this game object as selection and passes its' transform to the draw manager singleton
            }
        }
    }

    public void Select()
    {
        IsSelected = true;
        this.GetComponent<Renderer>().material = selectedMaterial;
    }
    private void Deselect()
    {
        IsSelected = false;
        this.GetComponent<Renderer>().material = originalMaterial;
    }

    //bool OnPlaneClick()
    //{
    //    //touch event object creation
    //    PointerEventData data = new PointerEventData(EventSystem.current);
    //    Touch touch = Input.GetTouch(0);
    //    data.position = touch.position;
    //    //{
    //    //    position = Input.mousePosition
    //    //};

    //    //raycast results list creation
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(data, results);
    //    //raycaster.Raycast(data, results);
        
    //    results.RemoveAll(r => r.gameObject.tag == "DetectedPlane");
        

    //    return results.Count > 0;
    //}

    //public void AssignPlane()
    //{
    //    //if this game object is selected...
    //    selectedPlane = this.gameObject;
    //    DrawManager.Instance.selectedPlane = selectedPlane.transform;
    //}


    //function linked to pointeronclick event
    //public void ToggleSelection()
    //{
    //    IsSelected = !IsSelected;

    //    if (IsSelected)
    //    {
    //        this.GetComponent<Renderer>().material = selectedMaterial;
    //    }
    //    else
    //    {
    //        this.GetComponent<Renderer>().material = originalMaterial;
    //    }
    //}



    //method called from SelectionResponse
    public void ToggleSelectedMaterial(Material selectionMaterial)
    {

        //check if plane selected = static variable for lastSelectedPlane
        //static variable for plane selected
        this.GetComponent<Renderer>().material = selectionMaterial;
    }
}