using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Agent
{
    [Header("Pursuer")]
    [SerializeField] private Player target;

    protected override void CalculateSteeringSum()
    {
        steeringForceSum += steering.Pursue(target);
        steeringForceSum += steering.Avoidance();
        steeringForceSum += steering.Separation();
    }
}
