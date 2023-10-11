using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prey : Agent
{
    [Header("Prey")]
    [SerializeField] private GameObject huntersRoot;
    [SerializeField] private float safeDistance;

    private List<Hunter> hunters;
    private Hunter currHunter;

    protected override void Awake()
    {
        base.Awake();
        hunters = new List<Hunter>();
        hunters = huntersRoot.GetComponentsInChildren<Hunter>().ToList();
    }

    protected override void CalculateSteeringSum()
    {
        steeringForceSum += steering.Separation();

        currHunter = GetClosestHunter();

        if (currHunter != null)
        {
            if (Vector3.Distance(transform.position, currHunter.GetPosition()) < safeDistance)
            {
                steeringForceSum += steering.Evade(currHunter);
            }
            else
            {
                steeringForceSum += steering.Wander();
            }
        }
    }

    private Hunter GetClosestHunter()
    {
        if (hunters.Count <= 0) return null;

        float currDistance = (transform.position - hunters[0].GetPosition()).magnitude;
        Hunter closestHunter = hunters[0];

        for (int i = 1; i < hunters.Count; i++)
        {
            float distance = (transform.position - hunters[i].GetPosition()).magnitude;
            if (distance < currDistance)
            {
                currDistance = distance;
                closestHunter = hunters[i];
            }
        }

        return closestHunter;
    }
}
