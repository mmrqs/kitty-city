using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of an IDLE cat
/// </summary>
[CreateAssetMenu(menuName = "FSM/Cat States Object/IDLE")]
public class IDLEcat : AbstractFSMState
{
    /// <summary>
    /// Duration of the inactivity of the cat
    /// </summary>
    private static float ldleduration = 3.0f;
    /// <summary>
    /// Timer
    /// </summary>
    private float timer = 0.0f;
    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.IDLE;
    }
    /// <summary>
    /// Enters in the IDLE state
    /// Play the idle animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.Play("Base Layer.ldle");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// while the cat is alive and the idle period is not over, we stay in this state
    /// if the idle period is over, we switch to the WALK state
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {                     
            if (_ennemy.Life <= 0)
                _fsm.EnterState(FSMStateType.DEAD);
            timer += Time.deltaTime;
            //we stay in the ldle state for 3 seconds
            if (timer > ldleduration)
                _fsm.EnterState(FSMStateType.WALK);
        }
    }

    public override bool ExitState()
    {
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>returns instance of IDLEcat</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<IDLEcat>();
    }
}
