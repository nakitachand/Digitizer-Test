using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARAnchorManager))]
public class DrawManager : Singleton<DrawManager>
{
    //public static DrawManager Instance;

    [SerializeField]
    private UnityEvent OnDraw = null;

    [SerializeField]
    Camera ARCamera;

    [SerializeField]
    private TraceLineSettings TraceLineSettings = null;

    private Dictionary<int, LineScript> TraceLines = new Dictionary<int, LineScript>();
    private int lineIndex = 0;

    public Transform selectedPlane;

    [SerializeField]
    private ARAnchorManager anchorManager;

    private List<ARAnchor> aRAnchors = new List<ARAnchor>();

    private bool CanDraw { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        anchorManager = GetComponent<ARAnchorManager>();
    }

    public void Update()
    {
        ////if (selectedPlane)
        ////{
        //    if (Input.touchCount > 0)
        //    {
        //        Touch touch = Input.GetTouch(0);
        //        Vector2 screenCenter = ScreenUtils.GetScreenCenter();
        //        Ray ray = ARCamera.ScreenPointToRay(screenCenter);
        //        RaycastHit hitObject;
        //        if (Physics.Raycast(ray, out hitObject))
        //        {
        //            Transform hitTransform = hitObject.transform;
        //        //if (hitTransform == selectedPlane)
        //        //{
        //        DebugManager.Instance.LogInfo($"hitTransform is {hitObject.point}, {CanDraw}");
        //        //Draw(hitObject.point);
        //        OnDrawTouch();
        //        //}
        //    }
        //    }
        ////}
        //OnDrawTouch();
    }

    public void AllowDraw(bool value)
    {
        CanDraw = value;
    }

    public void Draw(Vector3 drawPosition)
    {
        if(!CanDraw)
        {
            return;
        }

        if(TraceLines.Keys.Count == 0)
        {
            
            LineScript line = new LineScript(TraceLineSettings);
            TraceLines.Add(lineIndex, line);
            ARAnchor anchor = line.gameObject.AddComponent<ARAnchor>(); //anchorManager.AddAnchor(new Pose(drawPosition, Quaternion.identity));
            line.AddNewLineRenderer(this.transform, drawPosition, anchor);
            

            if(anchor == null)
            {
                DebugManager.Instance.LogInfo($"Error creating anchor.");
            }
            else
            {
                aRAnchors.Add(anchor);
            }
            
        }
        else
        {
            DebugManager.Instance.LogInfo($"{drawPosition}");
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
        GameObject[] Lines = GetAllLinesInScene();

        foreach(GameObject TraceLine in Lines)
        {
            LineRenderer line = TraceLine.GetComponent<LineRenderer>();
            Destroy(line);
        }
    }

    public void ShowLines()
    {
        foreach(var line in GetAllLinesInScene())
        {
            line.SetActive(true);
        }
    }

    public void HideLines()
    {
        foreach(var line in GetAllLinesInScene())
        {
            line.SetActive(false);
        }
    }

    public void OnDrawTouch()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 screenCenter = ScreenUtils.GetScreenCenter();
            Ray ray = ARCamera.ScreenPointToRay(screenCenter);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                Transform hitTransform = hitObject.transform;
                if (hitTransform == selectedPlane)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                            DebugManager.Instance.LogInfo($"screen touched");
                        OnDraw?.Invoke();
                            DebugManager.Instance.LogInfo($"OnDraw invoked");
                        LineScript line = new LineScript(TraceLineSettings);
                        TraceLines.Add(touch.fingerId, line);
                        DebugManager.Instance.LogInfo($"{hitObject.transform.position}");
                        ARAnchor anchor = line.gameObject.AddComponent<ARAnchor>();
                            
                        if (anchor == null)
                        {
                            DebugManager.Instance.LogInfo($"Error creating anchor.");
                        }
                        else
                        {
                            aRAnchors.Add(anchor);
                        }
                        
                        line.AddNewLineRenderer(this.transform, hitTransform.position, anchor);
                            DebugManager.Instance.LogInfo($"There should be lines.");
                    }
                    else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        TraceLines[touch.fingerId].AddPoint(hitTransform.position);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        TraceLines.Remove(touch.fingerId);
                    }
                }
            }
        }
    }
}

 
