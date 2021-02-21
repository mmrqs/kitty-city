using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Apple States Object/IDLE")]
public class IDLEApple : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        base.EnterState();
        
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {   
            if ()
                _fsm.EnterState(FSMStateType.PROJECTILE);
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<IDLEcat>();
    }
}
