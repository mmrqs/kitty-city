using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }
    public Vector3 Velocity { get; set; }

    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
        
    }

    private void OnDrawGizmos()
    {
        if (agentFlock == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agentFlock.neighborRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agentFlock.neighborRadius * agentFlock.avoidanceRadiusMultiplier);
    }
}
