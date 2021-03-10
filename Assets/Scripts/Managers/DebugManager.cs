using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : Singleton<DebugManager>
{
    
    [SerializeField]
    private Text text;

    [SerializeField]
    private int maxLines = 8;

    public void LogInfo (string message)
    {
        ClearLines();
        text.text += $"{message} \n";
    }

    private void ClearLines()
    {
        if(text.text.Split('\n').Length >= maxLines)
        {
            text.text = string.Empty;
        }
    }
}
