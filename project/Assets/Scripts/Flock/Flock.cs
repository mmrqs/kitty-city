using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();

    public FlockBehavior behavior;

    public Vector3 spawnValues;

    [Range(1, 250)]
    public int startingCount = 250;
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

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        avoidanceRadius = neighborRadius * avoidanceRadiusMultiplier;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Vector3 spawnPosition = new Vector3(spawnValues.x + Random.Range(-50f, 50f), 0.1f, spawnValues.z + Random.Range(-50f, 50f));
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                spawnPosition,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform);
            newAgent.name = "Cat " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearByObjects(agent);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > squareMaxSpeed)
                move = move.normalized * maxSpeed;

            agent.Velocity = move;
        }
    }

    List<Transform> GetNearByObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
