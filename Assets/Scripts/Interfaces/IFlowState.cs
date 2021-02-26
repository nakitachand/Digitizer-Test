using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlowState
{
    void StartState(bool force = false);
    void EndState(bool force = false);
}
