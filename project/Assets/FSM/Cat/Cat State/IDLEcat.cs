using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Cat States Object/IDLE")]
public class IDLEcat : AbstractFSMState
{
    private static float ldleduration = 3.0f;
    private float timer = 0.0f;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.Play("Base Layer.ldle");
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            timer += Time.deltaTime;

            //we stay in the ldle state for 3 seconds
            if(timer > ldleduration)
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
        return ScriptableObject.CreateInstance<IDLEcat>();
    }
}
