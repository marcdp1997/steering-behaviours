using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hunter : Agent
{
    [Header("Hunter")]
    [SerializeField] private int maxStamina;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material restingMaterial;
    [SerializeField] private Material huntingMaterial;
    [SerializeField] private GameObject preysRoot;

    private List<Prey> preys;
    private Prey currPrey;
    private bool resting;
    private int currStamina;

    protected override void Awake()
    {
        base.Awake();
        preys = new List<Prey>();
        preys = preysRoot.GetComponentsInChildren<Prey>().ToList();
    }

    protected override void CalculateSteeringSum()
    {
        meshRenderer.material = resting ? restingMaterial : huntingMaterial;

        steeringForceSum += steering.Avoidance();

        if (resting)
        {
            currStamina++;

            if (currStamina >= maxStamina) resting = false;
        }

        if (currPrey != null && !resting)
        {
            float distance = (transform.position - currPrey.GetPosition()).magnitude;
            if (distance < 2f)
            {
                preys.Remove(currPrey);
                Destroy(currPrey.gameObject);
            }

            steeringForceSum += steering.Pursue(currPrey);
            currStamina -= 2;

            if (currStamina <= 0)
            {
                currPrey = null;
                resting = true;
            }
        }
        else
        {
            steeringForceSum += steering.Wander();
            currPrey = GetClosestPrey();
        }
    }

    private Prey GetClosestPrey()
    {
        if (preys.Count <= 0) return null;

        float currDistance = (transform.position - preys[0].GetPosition()).magnitude;
        Prey closestPrey = preys[0];

        for (int i = 1; i < preys.Count; i++)
        {
            float distance = (transform.position - preys[i].GetPosition()).magnitude;
            if (distance < currDistance)
            {
                currDistance = distance;
                closestPrey = preys[i];
            }
        }

        return closestPrey;
    }
}
