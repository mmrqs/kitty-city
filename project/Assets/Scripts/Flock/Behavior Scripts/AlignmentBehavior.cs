using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow the flock agent to be aligned
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    /// <summary>
    /// Calculates the move of the flock agent according to the alignment principle
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="context">near by agents</param>
    /// <param name="flock">flock to which the agent belongs</param>
    /// <returns>a vector 3 of the next move for the agent</returns>
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        // Add all points together and make an average
        Vector3 alignmentMove = Vector3.zero;
        // select the near bodies according to a filter to avoid obstacles
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
            alignmentMove += (Vector3)item.transform.forward;

        // make an average from the forward vectors of near by agents
        alignmentMove /= context.Count;
        return alignmentMove;
    }
}