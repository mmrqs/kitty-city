using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Cat States Object/Chasing")]
public class ChasingCat : AbstractFSMState
{
    private NavMeshAgent catAgent;
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.CHASING;
    }

    public override bool EnterState()
    {
        base.EnterState();
        catAgent = _gameObject.GetComponent<NavMeshAgent>();
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            catAgent.destination = _ennemy.Target.position;
            if (!_ennemy.IsRabbitInSight)
                _fsm.EnterState(FSMStateType.WALK);
            if (_ennemy.Life <= 0)
                _fsm.EnterState(FSMStateType.DEAD);
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<ChasingCat>();
    }
}
