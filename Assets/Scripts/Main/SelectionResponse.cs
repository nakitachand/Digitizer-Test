using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//SelectionReponse implements the "Selection" and "Deselection" methods from ISelectionResponse
//passes in the transform of the plane in question
//and appropriately changes the material based on the value of "PlaneSelector.IsSelected"

public class SelectionResponse : MonoBehaviour, ISelectionResponse
{
    public Material selectedMaterial;
    public Material deSelectedMaterial;
    public void OnDeselect(Transform selection)
    {
        var selectedPlane = selection.GetComponent<PlaneSelector>();
        if (selectedPlane != null)
        {
            selectedPlane.IsSelected = false;
            selectedPlane.ToggleSelectedMaterial(deSelectedMaterial);
        }
    }
    public void OnSelect(Transform selection)
    {
        var selectedPlane = selection.GetComponent<PlaneSelector>();
        if (selectedPlane != null)
        {
            selectedPlane.IsSelected = true;
            selectedPlane.ToggleSelectedMaterial(selectedMaterial);
        }
    }
}