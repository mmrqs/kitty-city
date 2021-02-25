using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of an IDLE apple
/// </summary>
[CreateAssetMenu(menuName = "FSM/Apple States Object/IDLE")]
public class IDLEApple : AbstractFSMState
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
    /// change the layer mask of the apple
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _gameObject.layer = LayerMask.NameToLayer("IDLE");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// while the apple is on the ground, we stay in this state
    /// if the apple is in the air, it becomes a projectile
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {   
            if (_gameObject.transform.position.y > 0.74)
                _fsm.EnterState(FSMStateType.PROJECTILE);
        }
    }

    public override bool ExitState()
    {
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>instance of IDLEApple</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<IDLEApple>();
    }
}
