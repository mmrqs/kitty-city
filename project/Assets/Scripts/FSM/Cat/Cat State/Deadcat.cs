using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Cat States Object/Dead")]
public class Deadcat : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.DEAD;
    }

    public override bool EnterState()
    {
        base.EnterState();
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            FlockAgent _flockAgent = _gameObject.GetComponent<FlockAgent>();
            _flockAgent.RemoveMySelfFromFlock();
            Destroy(_gameObject);         
            Destroy(this);
            Destroy(_fsm);
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<Deadcat>();
    }
}
