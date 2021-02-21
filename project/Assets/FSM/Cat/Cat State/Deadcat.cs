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
