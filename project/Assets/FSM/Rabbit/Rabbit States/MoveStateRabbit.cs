using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Rabbit States Object/Move")]
public class MoveStateRabbit : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.WALK;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.SetInteger("AnimIndex", 1);
        _animator.SetTrigger("Next");
        EnteredState = true;
        return true;
    }

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
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<MoveStateRabbit>(); 
    }
}
