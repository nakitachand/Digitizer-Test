using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//[RequireComponent(typeof(ARAnchorManager))]
public class DrawManager : Singleton<DrawManager>
{
    [SerializeField]
    private UnityEvent OnDraw = null;

    [SerializeField]
    Camera ARCamera;

    [SerializeField]
    private TraceLineSettings TraceLineSettings = null;

    private Dictionary<int, LineScript> TraceLines = new Dictionary<int, LineScript>();
    private int lineIndex = 0;

    public Transform selectedPlane;

    private bool CanDraw { get; set; }

    private ARAnchor anchor;

    [SerializeField]
    private GameObject reticle;

    [SerializeField]
    private PointerHandler drawButton;

    private void Start()
    {
        Instantiate(reticle, new Vector3(Screen.width/2, Screen.height/2, 0), Quaternion.identity);
    }

    public void Update()
    {
        if (!CanDraw) { return; }
        DrawOnTouch();
    }
    public void ToggleAllowDraw()
    {
        CanDraw = !CanDraw;
        DebugManager.Instance.LogInfo($"{CanDraw}");
    }

    public void AllowDraw(PointerEventData data)
    {
        CanDraw = true;
        DebugManager.Instance.LogInfo($"Allow Draw called");
    }

    public void DontAllowDraw(PointerEventData data)
    {
        CanDraw = false;
        //DebugManager.Instance.LogInfo($"Dont Allow Draw called");
    }

    //linked to state change events
    public void AllowDraw(bool value)
    {
        CanDraw = value;
        //DebugManager.Instance.LogInfo($"{CanDraw}");
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
                //DebugManager.Instance.LogInfo($"hitTransform is {hitObject.point}, {CanDraw}");
                Draw(hitObject.point);
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            TraceLines.Remove(touch.fingerId);
        }
    }


    public void Draw(Vector3 drawPosition)
    {
        
        if (TraceLines.Keys.Count == 0)
        {
            LineScript line = new LineScript(TraceLineSettings);
            TraceLines.Add(lineIndex, line);
            line.AddNewLineRenderer(this.transform, drawPosition, anchor);
        }
        else
        {
            //DebugManager.Instance.LogInfo($"{drawPosition}");
            TraceLines[0].AddPoint(drawPosition);
        }

        OnDraw?.Invoke();
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
        //DebugManager.Instance.LogInfo($"CL Initiated");
        GameObject[] Lines = GetAllLinesInScene();
        //Lines[0] = TraceLines<0,0 >;
        //GetAllLinesInScene();
        //DebugManager.Instance.LogInfo($"{TraceLines.Count}");
        foreach (GameObject TraceLine in Lines)
        {
            //LineRenderer line = TraceLine.GetComponent<LineRenderer>();
            Destroy(TraceLine);
            //DebugManager.Instance.LogInfo($"line iteration completed");
        }
        ToggleAllowDraw();
        //DebugManager.Instance.LogInfo($"{CanDraw}, {FlowStateHandler.Instance.CurrentStateData.stateName}");

        drawButton.OnPointerDown.AddListener(AllowDraw);
        drawButton.OnPointerUp.AddListener(DontAllowDraw);

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


