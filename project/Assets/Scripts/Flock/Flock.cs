using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Models a flock behaviour
/// </summary>
public class Flock : MonoBehaviour
{
    /// <summary>
    /// Flock agent prefab
    /// </summary>
    public FlockAgent agentPrefab;
    /// <summary>
    /// List of all the flock agents in the flock
    /// </summary>
    public List<FlockAgent> agents = new List<FlockAgent>();

    /// <summary>
    /// Behaviour that the flock must follow
    /// </summary>
    public FlockBehavior behavior;

    /// <summary>
    /// Coordinates where the flock must spawn
    /// </summary>
    public Vector3 spawnValues;

    /// <summary>
    /// number of agents in the flock
    /// </summary>
    [Range(1, 250)]
    public int startingCount = 250;
    /// <summary>
    /// density of agents
    /// </summary>
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    protected float avoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    public float AvoidanceRadius { get { return avoidanceRadius; } }

    /// <summary>
    /// Start method
    /// instantiates the flock
    /// </summary>
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        avoidanceRadius = neighborRadius * avoidanceRadiusMultiplier;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            // instantiate agents in random position and rotation around a certain perimeter
            Vector3 spawnPosition = new Vector3(spawnValues.x + Random.Range(-50f, 50f), 0.1f, spawnValues.z + Random.Range(-50f, 50f));
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                spawnPosition,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform);
            newAgent.name = "Cat " + i;
            newAgent.Initialize(this);
            // add the agent to the flock
            agents.Add(newAgent);
        }
    }
    /// <summary>
    /// Update method
    /// At each frame, update the flock agent velocity
    /// </summary>
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            // get all the bodies near the agent
            List<Transform> context = GetNearByObjects(agent);
            // calculate the agent velocity
            Vector3 move = behavior.CalculateMove(agent, context, this);
            // multiply it with the drive factor
            move *= driveFactor;
            // if the vector magnitude is superior to the max speed, we normalized the vector
            if (move.sqrMagnitude > squareMaxSpeed)
                move = move.normalized * maxSpeed;
            // assign the velocity to the agent
            agent.Velocity = move;
        }
    }

    /// <summary>
    /// Get all the bodies near the agent
    /// </summary>
    /// <param name="agent">flock agent</param>
    /// <returns>all the near by bodies</returns>
    List<Transform> GetNearByObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // get all the colliders around the agent within a certain radius
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        // foreach colliders, if it's not the collider of the agent, we add it to the context of the agent
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
                context.Add(c.transform);
        }
        return context;
    }
}
