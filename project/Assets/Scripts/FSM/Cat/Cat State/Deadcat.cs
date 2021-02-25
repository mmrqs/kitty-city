using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of a dead cat
/// </summary>
[CreateAssetMenu(menuName = "FSM/Cat States Object/Dead")]
public class Deadcat : AbstractFSMState
{
    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.DEAD;
    }

    /// <summary>
    /// Enters in the Dead state
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// The agent is remove from its flock, we destroy its game object, this class and the fsm
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            FlockAgent _flockAgent = _gameObject.GetComponent<FlockAgent>();
            _flockAgent.RemoveMySelfFromFlock();
            Destroy(_gameObject);         
            Destroy(this);
            Destroy(_fsm);
        }
    }

    public override bool ExitState()
    {
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>instance of deadcat</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<Deadcat>();
    }
}
