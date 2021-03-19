using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//This script is attached to the ARPlane prefab to enable selection and deselection methods
//declared in the ISelectionResponse interface and implemented in the SelectionResponse class

public class PlaneSelector : MonoBehaviour
{
    private bool planeIsSelected;

    //private bool isLocked;

    [SerializeField]
    private Material selectedMaterial;

    [SerializeField]
    private Material originalMaterial;

    public static GameObject selectedPlane = null;

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

    //function linked to pointeronclick event
    public void ToggleSelection()
    {
        IsSelected = !IsSelected;

        if (IsSelected)
        {
            this.GetComponent<Renderer>().material = selectedMaterial;
        }
        else
        {
            this.GetComponent<Renderer>().material = originalMaterial;
        }
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

    //function called by planeselectionmanager
    public void ToggleSelectedMaterial(Material selectionMaterial)
    {

        //check if plane selected = static variable for lastSelectedPlane
        //static variable for plane selected
        this.GetComponent<Renderer>().material = selectionMaterial;
    }

    public void AssignPlane()
    {
        //if this game object is selected...
        selectedPlane = this.gameObject;
        DrawManager.Instance.selectedPlane = selectedPlane.transform;
    }
}