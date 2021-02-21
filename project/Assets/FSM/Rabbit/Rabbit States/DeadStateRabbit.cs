using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Rabbit States Object/Dead")]
public class DeadStateRabbit : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.DEAD;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.SetInteger("AnimIndex", 2);
        _animator.SetTrigger("Next");
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            // j'suis mort lol
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting idle state");
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<DeadStateRabbit>();
    }
}
