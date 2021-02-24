using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : Ennemy
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    public void Start()
    {
        EnnemyStart();
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock flock)
    {       
        agentFlock = flock;       
    }

    public void Update()
    {
        EnnemyUpdate();
    }
    /*private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }*/

    public void RemoveMySelfFromFlock()
    {
        agentFlock.agents.Remove(this);
    }
}
