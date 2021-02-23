using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Apple States Object/Projectile")]
public class ProjectileApple : AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PROJECTILE;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _gameObject.layer = LayerMask.NameToLayer("Projectile");
        EnteredState = true;
        return true;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (_gameObject.transform.position.y <= 0.74)
                _fsm.EnterState(FSMStateType.IDLE);
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<ProjectileApple>();
    }
}
