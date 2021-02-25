using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for the flock behaviour
/// </summary>
public abstract class FlockBehavior : ScriptableObject
{
    /// <summary>
    /// Calculates the move of the agent
    /// </summary>
    /// <param name="agent">agent</param>
    /// <param name="context">context of the agent</param>
    /// <param name="flock">flock to which the agent belongs</param>
    /// <returns></returns>
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
