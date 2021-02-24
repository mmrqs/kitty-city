using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Spider States Object/Chasing")]
public class ChasingSpider : AbstractFSMState
{
    private NavMeshAgent spiderAgent;
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.CHASING;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.Play("Base Layer.walk");
        spiderAgent = _gameObject.GetComponent<NavMeshAgent>();
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            spiderAgent.destination = _ennemy.Target.position;
            if (_ennemy.Life <= 0)
                _fsm.EnterState(FSMStateType.DEAD);
            if (_ennemy.IsRabbitInSight && Physics.OverlapSphere(_gameObject.transform.position, 4.5f, _ennemy.targetMask).Length != 0)
                _fsm.EnterState(FSMStateType.ATTACK);
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<ChasingSpider>();
    }
}
