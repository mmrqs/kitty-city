using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform target;

    public int life;

    public float viewRadius;
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

    protected void EnnemyUpdate()
    {
        SeeRabbit();
    }

    protected void EnnemyStart()
    {
        Life = life;
        Target = target;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PROJECTILE_MASK))
            Life -= Constants.DAMAGES_APPLE;
    }

    protected void SeeRabbit()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (Collider targetInView in targetsInViewRadius)
        {
            Transform target = targetInView.transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    IsRabbitInSight = true;
                else
                    IsRabbitInSight = false;
            }
        }
    }
}
