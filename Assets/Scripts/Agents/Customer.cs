using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Agent
{
    [Header("Customer")]
    [SerializeField] private Transform target;

    protected override void CalculateSteeringSum()
    {
        steeringForceSum += steering.Arrive(target.position);
        steeringForceSum += steering.Avoidance();
        steeringForceSum += steering.Queue(steeringForceSum);
        steeringForceSum += steering.Separation();
    }
}
