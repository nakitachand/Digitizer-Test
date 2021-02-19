using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    private static DebugManager _instance;
    public static DebugManager Instance
    {
        get
        { return _instance; }
    }
    [SerializeField]
    private Text text;


    public void Awake()
    {
        _instance = this;
    }

    public void LogInfo (string message)
    {
        text.text = $"{message}";
    }
}
