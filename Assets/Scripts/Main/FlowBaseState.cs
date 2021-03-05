using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//FlowState class attached to all State gameobjects in hierarchy
public class FlowBaseState : MonoBehaviour, IFlowState
{
    [SerializeField]
    private float nextStateDelay = 1.0f;
    public bool movingToNextState = false;

    [SerializeField]
    public UnityEvent OnStartState;

    [SerializeField]
    public UnityEvent OnEndState;

    [SerializeField]
    private float animationDelay;

    [SerializeField]
    private AnimationClip animationClip;

    //virtual method from NextState() in FlowStateHandler
    public virtual void NextState()
    {
        //calls NextState() base method in FlowStateHandler singleton 
        if(!movingToNextState)
        {
            FlowStateHandler.Instance.NextState(nextStateDelay);
        }

        movingToNextState = true;
    }

    //implementation from IFlowState
    public void EndState(bool force = false)
    {
        OnEndState?.Invoke();
        StopAllCoroutines();
    }

    //implementation from IFlowState
    public void StartState(bool force = false)
    {
        OnStartState?.Invoke();
        DebugManager.Instance.LogInfo($"{FlowStateHandler.Instance.CurrentStateData.stateName}");
        if(animationClip)
        {
            StartCoroutine(PlayAnimationDelay(animationDelay, animationClip));
        }
    }

    //playing animations - will need to pass in animation from animation manager
    private IEnumerator PlayAnimationDelay(float delay, AnimationClip animation)
    {
        yield return new WaitForSeconds(delay);
    }

    // Start is called before the first frame update
    //void Start()

    //{
    //    DebugManager.Instance.LogInfo($"{nextStateDelay}");
    //}
}
