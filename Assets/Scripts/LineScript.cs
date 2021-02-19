using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LineScript : MonoBehaviour
{
    private TraceLineSettings TraceLineSettings;

    public LineScript(TraceLineSettings settings)
    {
        this.TraceLineSettings = settings;
    }

    private Vector3 previousPointDistance = Vector3.zero;
    private LineRenderer LineRenderer
    {
        get;
        set;
    }

    public int positionCount = 0;

    public void AddPoint(Vector3 point)
    {
        if (previousPointDistance == null)
        {
            previousPointDistance = point;

        }

        if (previousPointDistance != null && Mathf.Abs(Vector3.Distance(previousPointDistance, point)) >= TraceLineSettings.MinDistanceBetweenPoints)
        {
            previousPointDistance = point;
            positionCount++;
            
            LineRenderer.positionCount = positionCount;
            
            LineRenderer.SetPosition(positionCount - 1, point);

            if (LineRenderer.positionCount % TraceLineSettings.SimplifyAfterPoints == 0 && TraceLineSettings.Simplification)
            {
                LineRenderer.Simplify(TraceLineSettings.Tolerance);
            }
        }

        
    }

    public void AddNewLineRenderer(Transform parent, Vector3 position) //, ARAnchor anchor)
    {
        positionCount = 2;

        GameObject newLine = new GameObject($"Trace Line");
        //newLine.transform.parent = anchor?.transform ?? parent;

        newLine.transform.position = position;
        newLine.tag = TraceLineSettings.GameObjectTag;

        LineRenderer line = newLine.AddComponent<LineRenderer>();
        line.startWidth = TraceLineSettings.StartWidth;
        line.endWidth = TraceLineSettings.EndWidth;
        line.startColor = TraceLineSettings.StartColor;
        line.endColor = TraceLineSettings.EndColor;
        line.material = TraceLineSettings.LineMaterial;
        line.numCornerVertices = TraceLineSettings.CornerVertices;
        line.numCapVertices = TraceLineSettings.EndCapVertices;
        line.useWorldSpace = true;
        line.positionCount = positionCount;
        line.SetPosition(0, position);
        line.SetPosition(1, position);
        LineRenderer = line;
        DebugManager.Instance.LogInfo($"New Line Renderer added.");
    }

}
