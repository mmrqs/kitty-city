using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    /// <summary>
    /// List of behaviours to apply to the flock
    /// </summary>
    public FlockBehavior[] behaviors;
    /// <summary>
    /// Weight of the behaviours
    /// </summary>
    public float[] weights;

    /// <summary>
    /// Calculates the move of the flock agent according to the behaviours
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <param name="context">near by agents</param>
    /// <param name="flock">flock to which the agent belongs</param>
    /// <returns>a Vector3 of the next move for the agent</returns>
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Handle data mismatch
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        //set up move
        Vector3 move = Vector3.zero;

        //iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            // we calculate the move of the agent according to the behaviour and multiply it by its weight
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;

            }
        }
        // we set the y to zero because the agents can't fly
        move.y = 0;
        return move;
    }
}