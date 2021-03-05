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

    private bool isLocked;

    [SerializeField]
    private Material selectedMaterial;

    [SerializeField]
    private Material originalMaterial;

    

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

    public bool Locked
    {
        get
        {
            return this.isLocked;
        }

        set
        {
            isLocked = value;
        }
    }

    public void ToggleSelection()
    {
        IsSelected = !IsSelected;

        if(IsSelected)
        {
            this.GetComponent<Renderer>().material = selectedMaterial;
        }
        else
        {
            this.GetComponent<Renderer>().material = originalMaterial;
        }
    }

    public void ToggleSelectedMaterial(Material selectionMaterial)
    {
        this.GetComponent<Renderer>().material = selectionMaterial;
    }

    //public void Highlight()
    //{
    //    this.GetComponent<Renderer>().material = highlightedPlane;
    //}

    //public void UnHighlight()
    //{
    //    this.GetComponent<Renderer>().material = unHighlightedPlane;

    //}

    //public void OnEnable()
    //{

    //}

    //public void OnDisable()
    //{

    //}


}