using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Manages the agent states
/// </summary>
public class FiniteStateMachine : MonoBehaviour
{
    AbstractFSMState _currentState;

    /// <summary>
    /// List of all the valid states
    /// </summary>
    public List<AbstractFSMState> _validStates;
    /// <summary>
    /// Dictionnary that links the states with their keyword
    /// </summary>
    Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

    public void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

        // we get the animator attached to the agent
        Animator animator = GetComponent<Animator>();
        // we get the ennemy
        Ennemy ennemy = GetComponent<Ennemy>();

        // foreach state in the list of states
        foreach (AbstractFSMState state in _validStates)
        {
            // we create an instance of this state 
            AbstractFSMState st = state.CreateInstance();
            st.SetExecutingFSM(this);
            st.SetExecutingAnimator(animator);
            st.SetExecutingFlockAgent(ennemy);
            st.SetExecutingGameObject(gameObject);
            _fsmStates.Add(st.StateType, st);
        }
    }

    /// <summary>
    /// Start method
    /// By default, we enter in the IDLE state
    /// </summary>
    void Start()
    {
        EnterState(_fsmStates[FSMStateType.IDLE]);
    }

    /// <summary>
    /// If there is a current state, we update it
    /// </summary>
    void Update()
    {
        if (_currentState != null)
            _currentState.UpdateState();
    }

    #region STATE MANAGEMENT
    /// <summary>
    /// Allow to enter in a state
    /// </summary>
    /// <param name="nextState"></param>
    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
            return;
        else if(_currentState != null)
            _currentState.ExitState();
        _currentState = nextState;
        _currentState.EnterState();
    }

    /// <summary>
    /// Allow to enter in a state according to its type
    /// </summary>
    /// <param name="stateType"></param>
    public void EnterState(FSMStateType stateType)
    {
        if (_fsmStates.ContainsKey(stateType))
        {
            AbstractFSMState nextState = _fsmStates[stateType];
            _currentState.ExitState();
            EnterState(nextState);
        }
    }

    #endregion
}
