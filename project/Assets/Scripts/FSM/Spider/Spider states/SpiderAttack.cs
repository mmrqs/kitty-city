using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of a spider attacking
/// </summary>
[CreateAssetMenu(menuName = "FSM/Spider States Object/Attack")]
public class SpiderAttack : AbstractFSMState
{
    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.ATTACK;
    }
    /// <summary>
    /// Enters in the attack state
    /// plays the attack animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.Play("Base Layer.attack");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// While the spider is alive and the rabbit is near, we stay in this state
    /// If the rabbit is not near, we hunt it ie we enter in the Chasing state
    /// If the spider is not alive, we enter in the dead state
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (_ennemy.Life <= 0)
                _fsm.EnterState(FSMStateType.DEAD);
            if (Physics.OverlapSphere(_gameObject.transform.position, 4.5f, _ennemy.targetMask).Length == 0)
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
    /// <returns>instance of SpiderAttack</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<SpiderAttack>();
    }
}
