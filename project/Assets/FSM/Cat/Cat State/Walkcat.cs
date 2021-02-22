using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Cat States Object/Walk")]
public class Walkcat : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.WALK;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.Play("Base Layer.walk");
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            Vector3 velocity = _flockAgent.Velocity;
            _gameObject.transform.forward = velocity.normalized;
            _gameObject.transform.position += velocity * Time.deltaTime;

            if (_flockAgent.Hurt)
                _fsm.EnterState(FSMStateType.DEAD);
            if (_flockAgent.IsRabbitInSight)
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
        return ScriptableObject.CreateInstance<Walkcat>();
    }
}