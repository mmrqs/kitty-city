using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FSM/Rabbit States Object/IDLE")]
public class IDLEStateRabbit : AbstractFSMState
{

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.SetInteger("AnimIndex", 0);
        _animator.SetTrigger("Next");
        EnteredState = true;
        return true;
    }

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
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<IDLEStateRabbit>();
    }
}