using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow the flock agent to avoid collision with other flock agents
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    /// <summary>
    /// Calculates the move of the flock agent according to the avoidance principle.
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="context">near by agents</param>
    /// <param name="flock">flock to which the agent belongs</param>
    /// <returns>a vector 3 of the next move for the agent</returns>
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        // Add all points together and make an average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        // select the near bodies according to a filter to avoid obstacles
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            // uses the closest point as some obstacles are huge ie houses
            Vector3 closestPoint = item.GetComponent<Collider>().ClosestPoint(agent.transform.position);

            // if the obstacle is close
            if (((closestPoint - agent.transform.position)).sqrMagnitude < flock.SquareAvoidanceRadius)
            {
                // we make the agent avoid the obstacle
                Vector3 distance = agent.transform.position - closestPoint;
                nAvoid++;
                avoidanceMove += distance.normalized * flock.AvoidanceRadius - distance;
            }
        }
        // we average the avoidance with the number of obstacles to avoid
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}