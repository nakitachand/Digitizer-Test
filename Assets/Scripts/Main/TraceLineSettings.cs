using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "Trace Line Settings", menuName = "Create Trace Line Settings", order = 0)]
public class TraceLineSettings : ScriptableObject
{
    public string GameObjectTag = "Line";

    [Header("Line Properties")]
    public Color StartColor = Color.black;
    public Color EndColor = Color.black;
    public int CornerVertices = 10;
    public int EndCapVertices = 10;
    public float StartWidth = 0.01f;
    public float EndWidth = 0.01f;
    public float MaxDistanceFromCamera = 2f;
    public Material LineMaterial;

    [Range(0f, 1f)]
    public float MinDistanceBetweenPoints = 0.001f;

    [Header("Line Tolerances")]
    public bool Simplification = false;
    public float Tolerance = 0.001f;
    public float SimplifyAfterPoints = 20f;
    public bool allowMultipleTouch = true;
}
