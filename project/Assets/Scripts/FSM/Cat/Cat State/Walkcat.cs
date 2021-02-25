using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of a walk cat
/// </summary>
[CreateAssetMenu(menuName = "FSM/Cat States Object/Walk")]
public class Walkcat : AbstractFSMState
{
    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.WALK;
    }
    /// <summary>
    /// Enters the WALK state
    ///Plays the walk animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.Play("Base Layer.walk");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// We stay in this state while the cat is alive and the rabbit not in sight
    /// for each frame, we get the velocity of the cat and make it move according to it
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            
            Vector3 velocity = _ennemy.Velocity;
            if (velocity != Vector3.zero)
            {
                _gameObject.transform.forward = velocity.normalized;
                _gameObject.transform.position += velocity * Time.deltaTime;
            }
            if (_ennemy.Life <= 0)
                _fsm.EnterState(FSMStateType.DEAD);
            if (_ennemy.IsRabbitInSight)
                _fsm.EnterState(FSMStateType.CHASING);
        }
    }

    public override bool ExitState()
    {
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>return instance of Walkcat</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<Walkcat>();
    }
}