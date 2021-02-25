using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of an IDLE rabbit
/// </summary>
[CreateAssetMenu(menuName = "FSM/Rabbit States Object/IDLE")]
public class IDLEStateRabbit : AbstractFSMState
{
    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.IDLE;
    }

    /// <summary>
    /// Enters in the IDLE state
    /// Play the IDLE animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.SetInteger("AnimIndex", 0);
        _animator.SetTrigger("Next");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// If the key Z is pressed, we enter in the WALK state
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                _fsm.EnterState(FSMStateType.WALK);
        }
    }

    public override bool ExitState()
    {
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>returns instance of IDLEStateRabbit</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<IDLEStateRabbit>();
    }
}