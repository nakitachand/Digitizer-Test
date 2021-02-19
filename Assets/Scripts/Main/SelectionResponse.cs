using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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