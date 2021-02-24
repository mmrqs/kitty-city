using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Spider States Object/Attack")]
public class SpiderAttack : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.ATTACK;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.Play("Base Layer.attack");
        EnteredState = true;
        return true;
    }

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
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<SpiderAttack>();
    }
}
