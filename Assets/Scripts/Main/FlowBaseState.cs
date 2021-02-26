using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowBaseState : MonoBehaviour, IFlowState
{
    [SerializeField]
    private float nextStateDelay = 1.0f;
    public bool movingToNextState = false;

    public virtual void NextState()
    {

    }
    
    public void EndState(bool force = false)
    {
        throw new System.NotImplementedException();
    }

    public void StartState(bool force = false)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()

    {
        DebugManager.Instance.LogInfo($"{nextStateDelay}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
