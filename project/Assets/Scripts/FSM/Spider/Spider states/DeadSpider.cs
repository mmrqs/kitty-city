using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Spider States Object/Dead")]
public class DeadSpider : AbstractFSMState
{
    private static float ldleduration = 2.0f;
    private float timer = 0.0f;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.DEAD;
    }

    public override bool EnterState()
    {
        base.EnterState();
        _animator.Play("Base Layer.die");
        EnteredState = true;
        return true;
    }

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
        base.ExitState();
        return true;
    }

    public override AbstractFSMState CreateInstance()
    {
        return ScriptableObject.CreateInstance<DeadSpider>();
    }
}
