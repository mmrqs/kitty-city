using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow the flock agent in groups with other agents
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;

    /// <summary>
    /// Calculates the move of the flock agent according to the cohesion principle
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="context">near by agents</param>
    /// <param name="flock">flock to which the agent belongs</param>
    /// <returns>a vector3 of the next move for the agent</returns>
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        // Add all points together and average
        Vector3 cohesionMove = Vector3.zero;
        // select the near bodies according to a filter to avoid obstacles
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        // foreach agents near by the agent, we add their position
        foreach (Transform item in filteredContext)
            cohesionMove += (Vector3)item.position;

        // we make an average of the movement of the nearby agent
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= (Vector3)agent.transform.position;
        // smooth the vector
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
