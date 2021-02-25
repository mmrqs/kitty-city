using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State of a dead spider
/// </summary>
[CreateAssetMenu(menuName = "FSM/Spider States Object/Dead")]
public class DeadSpider : AbstractFSMState
{
    /// <summary>
    /// Duration of the dead period
    /// </summary>
    private static float ldleduration = 2.0f;
    private float timer = 0.0f;

    /// <summary>
    /// Called when the state is loaded
    /// </summary>
    public override void OnEnable()
    {
        StateType = FSMStateType.DEAD;
    }

    /// <summary>
    /// Enters the state
    /// Plays the die animation
    /// </summary>
    /// <returns></returns>
    public override bool EnterState()
    {
        _animator.Play("Base Layer.die");
        EnteredState = true;
        return true;
    }

    /// <summary>
    /// We wait until the timer is over and then we remove the gameobject and this class
    /// </summary>
    public override void UpdateState()
    {
        if (EnteredState)
        {
            timer += Time.deltaTime;
            //we stay in the ldle state for 3 seconds
            if (timer > ldleduration)
            {
                Destroy(_gameObject);
                Destroy(this);
                Destroy(_fsm);
            }

        }
    }

    public override bool ExitState()
    {
        return true;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>instance of DeadSpider</returns>
    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<DeadSpider>();
    }
}
