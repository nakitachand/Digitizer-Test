using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//script to select a detected plane by touch
//needs to have a function return a pose or transform for the lastSelectedPlane

public class PlaneSelector : MonoBehaviour
{
    private bool planeIsSelected;
    public Material highlightedPlane;
    public Material unHighlightedPlane;

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

    public void Highlight()
    {
        this.GetComponent<Renderer>().material = highlightedPlane;
    }

    public void UnHighlight()
    {
        this.GetComponent<Renderer>().material = unHighlightedPlane;

    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void ToggleSelectedMaterial(Material selectionMaterial)
    {
        this.GetComponent<Renderer>().material = selectionMaterial;
    }
}