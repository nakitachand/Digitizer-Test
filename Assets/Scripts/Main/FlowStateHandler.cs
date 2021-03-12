using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Single instance Flow State Manager handling all
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

    public bool IsShuttingDown
    {
        get;
        private set;
    }

    private void Awake()
    {
        IsShuttingDown = false;
    }

    private void SetGameObjectsActive(GameObject[] gameObjects, bool active = true)
    {
        foreach(GameObject game in gameObjects)
        {
            game.SetActive(active);
            if(game.GetComponent<IFlowState>() != null)
            {
                if(active)
                {
                    game.GetComponent<IFlowState>().StartState();
                }
                else
                {
                    game.GetComponent<IFlowState>().EndState();
                }
            }
        }
    }

    private void SetState(int state)
    {
        if(state == CurrentState && CurrentStateData != null )
        {
            return;
        }

        if(CurrentStateData != null)
        {
            SetGameObjectsActive(CurrentStateData.deactivateOnStateEnd, false);
        }

        CurrentState = state;

        CurrentStateData = stateOrder[state];

        SetGameObjectsActive(CurrentStateData.activateOnStateStart, true);
    }    

    //function that adds 
    public void NextState(float delay)
    {
        StartCoroutine(InternalNextState(delay));
    }

    private IEnumerator InternalNextState(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        //checks that current state is not the last state
        if(CurrentState+1 < stateOrder.Length)
        {
            //increments and sets current state value to next state value
            SetState(CurrentState + 1);
        }
    }

    public void GoToHomeState(float delay)
    {
        SetState(0);
    }

    public void PreviousState(float delay)
    {
        StartCoroutine(InternalPrevState(delay));
    }

    private IEnumerator InternalPrevState(float delay)
    {
        yield return new WaitForSeconds(delay);

        //checks that current state is not the first state
        //if (CurrentState - 1 < 0)
        //{
            //increments and sets current state value to next state value
            SetState(CurrentState - 1);
        //}
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
    public GameObject[] deactivateOnStateEnd;
}