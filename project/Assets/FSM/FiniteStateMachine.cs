using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FiniteStateMachine : MonoBehaviour
{
    public AbstractFSMState _startingState;
    AbstractFSMState _currentState;

    public List<AbstractFSMState> _validStates;
    Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

    public void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();
        
        FlockAgent flockAgent = GetComponent<FlockAgent>();
        Animator animator = GetComponent<Animator>();


        foreach (AbstractFSMState state in _validStates)
        {
            AbstractFSMState st = state.CreateInstance();
            st.SetExecutingFSM(this);
            st.SetExecutingAnimator(animator);
            st.SetExecutingFlockAgent(flockAgent);
            st.SetExecutingGameObject(gameObject);
            _fsmStates.Add(st.StateType, st);
        }
    }

    void Start()
    {
        if (_startingState != null)
            EnterState(_fsmStates[FSMStateType.IDLE]);
    }

    void Update()
    {
        if (_currentState != null)
            _currentState.UpdateState();
    }

    #region STATE MANAGEMENT

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
            return;
        else if(_currentState != null)
            _currentState.ExitState();
        _currentState = nextState;
        _currentState.EnterState();
    }

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
