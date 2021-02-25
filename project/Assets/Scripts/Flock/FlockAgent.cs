using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Models a flock agent
/// </summary>
[RequireComponent(typeof(Collider))]
public class FlockAgent : Ennemy
{
    /// <summary>
    /// flock of the agent
    /// </summary>
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    /// <summary>
    /// start method
    /// get the collider of the agent
    /// </summary>
    public void Start()
    {
        EnnemyStart();
        agentCollider = GetComponent<Collider>();
    }

    /// <summary>
    /// Initializes the flock
    /// </summary>
    /// <param name="flock"></param>
    public void Initialize(Flock flock)
    {       
        agentFlock = flock;       
    }

    /// <summary>
    /// update the agent
    /// </summary>
    public void Update()
    {
        EnnemyUpdate();
    }

    /// <summary>
    /// Remove the agent from the flock
    /// </summary>
    public void RemoveMySelfFromFlock()
    {
        agentFlock.agents.Remove(this);
    }
}
