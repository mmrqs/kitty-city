using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// States type
/// </summary>
public enum FSMStateType
{
    IDLE,
    WALK,
    DEAD,
    ATTACK,
    PROJECTILE,
    CHASING
};

/// <summary>
/// Abstract class for states
/// </summary>
public abstract class AbstractFSMState : ScriptableObject
{
    protected FiniteStateMachine _fsm;
    protected Animator _animator;
    protected GameObject _gameObject;
    protected Ennemy _ennemy;

    // indicates if the agent has entered in the state
    public bool EnteredState { get; protected set; }
    // type of the state
    public FSMStateType StateType { get; protected set; }

    // function called when the object is loaded
    public abstract void OnEnable();
    // function called when the agent enters in the state
    public abstract bool EnterState();
    // function called when the agent is in the state
    public abstract void UpdateState();
    // function called when the agent exit the state
    public abstract bool ExitState();

    /// <summary>
    /// Initialize the fsm
    /// </summary>
    /// <param name="fsm"></param>
    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        _fsm = fsm;
    }
    /// <summary>
    /// Initialize the animator
    /// </summary>
    /// <param name="animator"></param>
    public virtual void SetExecutingAnimator(Animator animator)
    {
        if (animator != null)
            _animator = animator;
    }
    /// <summary>
    /// Initialize the game object
    /// </summary>
    /// <param name="go"></param>
    public virtual void SetExecutingGameObject(GameObject go)
    {
        if (go != null)
            _gameObject = go;
    }
    /// <summary>
    /// Initialize the ennemy
    /// </summary>
    /// <param name="agent"></param>
    public virtual void SetExecutingFlockAgent(Ennemy agent)
    {
        if(agent != null)
            _ennemy = agent;
    }
    /// <summary>
    /// Return an instance of the state
    /// </summary>
    /// <returns></returns>
    public abstract AbstractFSMState CreateInstance();
}
