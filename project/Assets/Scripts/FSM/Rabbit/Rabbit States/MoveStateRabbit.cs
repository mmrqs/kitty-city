using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of the rabbit moving
/// </summary>
[CreateAssetMenu(menuName = "FSM/Rabbit States Object/Move")]
public class MoveStateRabbit : AbstractFSMState
{
    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.WALK;
    }
    /// <summary>
    /// Enters in the Walk state
    /// Plays the walk animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.SetInteger("AnimIndex", 1);
        _animator.SetTrigger("Next");
        EnteredState = true;
        return true;
    }
    /// <summary>
    /// If the key A is pressed, we enter in the IDLE state
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Input.GetKey(KeyCode.A))
                _fsm.EnterState(FSMStateType.IDLE);
        }
    }

    public override bool ExitState()
    {
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>instance of MoveStateRabbit</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<MoveStateRabbit>(); 
    }
}
