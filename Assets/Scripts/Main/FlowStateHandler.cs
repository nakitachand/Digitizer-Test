using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowStateHandler : Singleton<FlowStateHandler>
{
    [SerializeField]
    private FlowStateData[] stateOrder;

    public int CurrentState
    {
        get;
        private set;
    }

    public FlowStateData CurrentStateData
    {
        get;
        private set;
    }

    public bool isShuttingDown
    {
        get;
        private set;
    }

    private void Awake()
    {
        isShuttingDown = false;
    }

    private void SetState(int state)
    {

    }    

    public void NextState(float delay)
    {

    }

    private IEnumerator InternalNextState(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetState(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class FlowStateData
{
    public string stateName = "";

    [SerializeField]
    public GameObject[] activateOnStateStart;

    [SerializeField]
    public GameObject[] activateOnStateEnd;
}