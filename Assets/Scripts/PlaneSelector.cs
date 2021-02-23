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