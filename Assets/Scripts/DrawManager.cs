using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    }

    public void Update()
    {
        OnDrawTouch();
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
            line.AddNewLineRenderer(this.transform, drawPosition);
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
                if(hitTransform == selectedPlane)
                {
                    Draw(hitObject.point);
                }
            }
        }
    }
}

 
