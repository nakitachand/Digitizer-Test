using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class DrawManager : MonoBehaviour
{
    public static DrawManager Instance;

    [SerializeField]
    private UnityEvent OnDraw = null;

    [SerializeField]
    Camera ARCamera;

    [SerializeField]
    private TraceLineSettings TraceLineSettings = null;

    private Dictionary<int, LineScript> TraceLines = new Dictionary<int, LineScript>();
    private int lineIndex = 0;

    public Transform selectedPlane;

    private ARAnchorManager anchorManager;

    private List<ARAnchor> aRAnchors = new List<ARAnchor>();

    private bool CanDraw
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        anchorManager = GetComponent<ARAnchorManager>();


    }

    public void Update()
    {
        if (selectedPlane)
        {
            if (Input.touchCount > 0)
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
                        Draw(hitObject.point);
                    }
                }
            }
        }
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
            line.AddNewLineRenderer(this.transform, drawPosition); //, anchor);
            

            if(anchor == null)
            {
                Debug.LogError("Error creating anchor.");
            }
            else
            {
                aRAnchors.Add(anchor);
            }
            
        }
        else
        {
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
                        OnDraw?.Invoke();
                        LineScript line = new LineScript(TraceLineSettings);
                        TraceLines.Add(touch.fingerId, line);
                        //ARAnchor anchor = line.gameObject.AddComponent<ARAnchor>(); //anchorManager.AddAnchor(new Pose(hitTransform.position, Quaternion.identity));
                        //if (anchor == null)
                        //{
                        //    Debug.LogError("Error creating anchor.");
                        //}
                        //else
                        //{
                        //    aRAnchors.Add(anchor);
                        //}

                        line.AddNewLineRenderer(this.transform, hitTransform.position); //, anchor);
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

 
