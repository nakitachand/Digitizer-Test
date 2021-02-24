using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

[RequireComponent(typeof(ARAnchorManager))]
public class DrawManager : Singleton<DrawManager>
{
    [SerializeField]
    private UnityEvent OnDraw = null;

    //[SerializeField]
    //private UnityEvent OnButtonHold;

    //[SerializeField]
    //private UnityEvent OnButtonRelease;

    [SerializeField]
    Camera ARCamera;

    [SerializeField]
    private TraceLineSettings TraceLineSettings = null;

    private Dictionary<int, LineScript> TraceLines = new Dictionary<int, LineScript>();
    private int lineIndex = 0;

    public Transform selectedPlane;

    private bool CanDraw { get; set; }

    private ARAnchor anchor;

    //[SerializeField]
    //private Button drawButton;

    //private bool isPressed = false;

    // Start is called before the first frame update
    //void Awake()
    //{

    //}

    public void Update()
    {
        
        DrawOnTouch();
    }

    public void AllowDraw(bool value)
    {
        CanDraw = value;
    }

    public void Draw(Vector3 drawPosition)
    {
        if (!CanDraw) { return; }

        if (TraceLines.Keys.Count == 0)
        {
            LineScript line = new LineScript(TraceLineSettings);
            TraceLines.Add(lineIndex, line);
            line.AddNewLineRenderer(this.transform, drawPosition, anchor);
        }
        else
        {
            DebugManager.Instance.LogInfo($"{drawPosition}");
            TraceLines[0].AddPoint(drawPosition);
        }

        OnDraw?.Invoke();
    }

    public void DrawOnTouch()
    {
        
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            Vector2 screenCenter = ScreenUtils.GetScreenCenter();
            Ray ray = ARCamera.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hitObject))
            {
                DebugManager.Instance.LogInfo($"hitTransform is {hitObject.point}, {CanDraw}");
                Draw(hitObject.point);
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            TraceLines.Remove(touch.fingerId);
        }
    }

    public void StopDrawing()
    {
        TraceLines.Remove(0);
    }

    private GameObject[] GetAllLinesInScene()
    {
        return GameObject.FindGameObjectsWithTag("Line");
    }

    public void ClearLines()
    {
        GameObject[] Lines = GetAllLinesInScene();

        foreach (GameObject TraceLine in Lines)
        {
            LineRenderer line = TraceLine.GetComponent<LineRenderer>();
            Destroy(line);
        }
    }

    public void ShowLines()
    {
        foreach (var line in GetAllLinesInScene())
        {
            line.SetActive(true);
        }
    }

    public void HideLines()
    {
        foreach (var line in GetAllLinesInScene())
        {
            line.SetActive(false);
        }
    }

}


