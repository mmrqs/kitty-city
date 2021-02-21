using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED
};

public enum FSMStateType
{
    IDLE,
    WALK,
    DEAD,
    ATTACK,
    PROJECTILE
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected FiniteStateMachine _fsm;
    protected Animator _animator;
    protected GameObject _gameObject;
    protected FlockAgent _flockAgent;

    public ExecutionState ExecutionState { get; protected set; }
    public bool EnteredState { get; protected set; }
    public FSMStateType StateType { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        ExecutionState = ExecutionState.ACTIVE;
        return (_animator != null);
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        _fsm = fsm;
    }

    public virtual void SetExecutingAnimator(Animator animator)
    {
        if (animator != null)
            _animator = animator;
    }

    public virtual void SetExecutingGameObject(GameObject go)
    {
        if (go != null)
            _gameObject = go;
    }

    public virtual void SetExecutingFlockAgent(FlockAgent agent)
    {
        if(agent != null)
            _flockAgent = agent;
    }

    public abstract AbstractFSMState CreateInstance();
}
