using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// State of a spider chasing
/// </summary>
[CreateAssetMenu(menuName = "FSM/Spider States Object/Chasing")]
public class ChasingSpider : AbstractFSMState
{
    /// <summary>
    /// NavMeshAgent component of the spider
    /// </summary>
    private NavMeshAgent spiderAgent;

    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.CHASING;
    }
    /// <summary>
    /// Enters in the chasing state, 
    /// Plays the walk animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.Play("Base Layer.walk");
        spiderAgent = _gameObject.GetComponent<NavMeshAgent>();
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// While the rabbit is not in sight and the spider is alive, we stay in this state
    /// if the spider is no longer alive, we enter is the dead state
    /// if the rabbit is in sight and near, we enter in the ATTACK state
    /// </summary>
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
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>instance of chasingspider</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<ChasingSpider>();
    }
}
