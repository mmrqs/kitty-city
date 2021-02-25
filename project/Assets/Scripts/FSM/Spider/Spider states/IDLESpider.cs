using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of an IDLESpider
/// </summary>
[CreateAssetMenu(menuName = "FSM/Spider States Object/IDLE")]
public class IDLESpider : AbstractFSMState
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
    /// Plays the IDLE animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.Play("Base Layer.ldle");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// While the spider is alive and the rabbit is not in sight we stay in this state
    /// if the spider is not alive anymore, we enter in the DEAD state
    /// if the rabbit is in sight, we enter in the CHASING state
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (_ennemy.Life <= 0)
                _fsm.EnterState(FSMStateType.DEAD);
            if (_ennemy.IsRabbitInSight)
                _fsm.EnterState(FSMStateType.CHASING);
        }
    }

    public override bool ExitState()
    {
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>instance of IDLESpider</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<IDLESpider>();
    }
}