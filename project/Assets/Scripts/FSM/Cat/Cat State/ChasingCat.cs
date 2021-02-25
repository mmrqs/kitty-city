using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// State of a chasing cat
/// </summary>
[CreateAssetMenu(menuName = "FSM/Cat States Object/Chasing")]
public class ChasingCat : AbstractFSMState
{
    /// <summary>
    /// NavMeshAgent of the cat
    /// </summary>
    private NavMeshAgent catAgent;

    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.CHASING;
    }

    /// <summary>
    /// Enters in the chasing state
    /// get the navmeshagent attached to the flock agent
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        catAgent = _gameObject.GetComponent<NavMeshAgent>();
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// While the rabbit is in sight and the flock agent is alive, we stay in this state and  the destination of the 
    /// cat is the target position (ie player position).
    /// If the player is no longer in sight, we switch to the walk state
    /// If the ennemy is not alive anymore, we switch to the dead state
    /// </summary>
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
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>instance of chasingcat</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<ChasingCat>();
    }
}
