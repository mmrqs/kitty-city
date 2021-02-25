using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Models an ennemy behaviour
/// </summary>
public class Ennemy : MonoBehaviour
{
    /// <summary>
    /// mask of the target of the ennemy (the player)
    /// </summary>
    public LayerMask targetMask;
    /// <summary>
    /// Mask of the obstacles
    /// </summary>
    public LayerMask obstacleMask;
    /// <summary>
    /// Target of the ennemy (the player)
    /// </summary>
    public Transform target;

    /// <summary>
    /// Ennemy life
    /// </summary>
    public int life;

    /// <summary>
    /// indicates how far the ennemy can see
    /// </summary>
    public float viewRadius;
    /// <summary>
    /// View angle of the ennemy
    /// </summary>
    [Range(0, 360)]
    public float viewAngle;

    public bool IsRabbitInSight { get; set; }
    public int Life { get; private set; }
    public Transform Target { get; private set; }
    public Vector3 Velocity { get; set; }

    void Start()
    {
        EnnemyStart();
    }

    void Update()
    {
        EnnemyUpdate();
    }
    /// <summary>
    /// At each frame, we check if the ennemy sees the player.
    /// </summary>
    protected void EnnemyUpdate()
    {
        SeeRabbit();
    }

    /// <summary>
    /// Initializes the life of the ennemy and its target
    /// </summary>
    protected void EnnemyStart()
    {
        Life = life;
        Target = target;
    }

    /// <summary>
    /// triggered when the ennemy enters in collision with a body
    /// </summary>
    /// <param name="collision">collision informations</param>
    void OnCollisionEnter(Collision collision)
    {
        // if the cat is touched by an apple
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PROJECTILE_MASK))
            Life -= Constants.DAMAGES_APPLE;
    }

    /// <summary>
    /// Indicates wether the ennemy can see the player.
    /// </summary>
    protected void SeeRabbit()
    {
        // gets all the collider in the view radius that have the target mask (Player layer)
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // foreach collider
        foreach (Collider targetInView in targetsInViewRadius)
        {
            Transform target = targetInView.transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;
            // if the player is in the angle of sight of the ennemy
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                // if there is no obstacles between the ennemy and the player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    IsRabbitInSight = true;
                else
                    IsRabbitInSight = false;
            }
        }
    }
}
