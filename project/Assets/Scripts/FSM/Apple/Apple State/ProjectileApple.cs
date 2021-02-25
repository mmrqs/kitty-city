using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of a projectile apple
/// </summary>
[CreateAssetMenu(menuName = "FSM/Apple States Object/Projectile")]
public class ProjectileApple : AbstractFSMState
{
    /// <summary>
    /// Called when the state is loaded and indicates that this is the projectile state
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.PROJECTILE;
    }
    /// <summary>
    /// Enters in the projectile state
    /// change the layer mask of the apple to indicates that it is now a projectile.
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _gameObject.layer = LayerMask.NameToLayer("Projectile");
        EnteredState = true;
        return true;
    }
    /// <summary>
    /// While the apple is in the air, we stay in this state
    /// if the apple is in the floor, it becomes IDLE so we change the state
    /// </summary>
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
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns an instance of ProjectileApple</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<ProjectileApple>();
    }
}
