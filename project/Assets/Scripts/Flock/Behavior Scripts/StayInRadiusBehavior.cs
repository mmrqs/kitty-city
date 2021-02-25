using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow the agent to stay in a certain radius
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    // center of the perimeter
    public Vector3 center;
    /// <summary>
    /// Radius
    /// </summary>
    public float radius = 15f;

    /// <summary>
    /// Calculates the move of the flock agent according to the Stay In Radius principle
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="context">near by agents</param>
    /// <param name="flock">flock to which the agent belongs</param>
    /// <returns>a vector 3 of the next move for the agent</returns>
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // we determine the distance between the agents and the center
        Vector3 centerOffset = center - agent.transform.position;
        // the vector length divided by the radius
        float t = centerOffset.magnitude / radius;
        // if the agent is still in the allowed perimeter, we don't change its trajectory
        if (t < 0.9f)
            return Vector3.zero;
        // if not, we modify its trajectory
        return centerOffset * t * t;
    }
}
