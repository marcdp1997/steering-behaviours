using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Agent
{
    [Header("Prey")]
    [SerializeField] private Hunter hunter;
    [SerializeField] private float safeDistance;

    protected override void CalculateSteeringSum()
    {
        steeringForceSum += steering.Avoidance();
        steeringForceSum += steering.Separation();

        if (hunter != null)
        {
            if (Vector3.Distance(transform.position, hunter.GetPosition()) < safeDistance)
            {
                steeringForceSum += steering.Evade(hunter);
            }
            else
            {
                steeringForceSum += steering.Wander();
            }
        }
    }
}
