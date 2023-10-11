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
    [SerializeField] private GameObject huntersRoot;

    private List<Prey> preys;
    private Prey currPrey;
    private bool resting = true;
    private int currStamina;

    protected override void Awake()
    {
        base.Awake();
        preys = new List<Prey>();
        preys = huntersRoot.GetComponentsInChildren<Prey>().ToList();
        currStamina = Random.Range(0, maxStamina / 2);
    }

    protected override void CalculateSteeringSum()
    {
        meshRenderer.material = resting ? restingMaterial : huntingMaterial;

        if (resting)
        {
            currStamina++;

            if (currStamina >= maxStamina) resting = false;
        }
        else currPrey = GetClosestPrey();

        if (currPrey != null && !resting)
        {
            float distance = (transform.position - currPrey.GetPosition()).magnitude;
            if (distance < 2f)
            {
                preys.Remove(currPrey);
                Destroy(currPrey.gameObject);
                currPrey = GetClosestPrey();
            }

            steeringForceSum += steering.Pursuit(currPrey);
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
        }
    }

    private Prey GetClosestPrey()
    {
        if (preys.Count <= 0) return null;

        float currDistance = 0;
        Prey closestPrey = null;

        for (int i = 0; i < preys.Count; i++)
        {
            if (preys[i] != null)
            {
                float distance = (transform.position - preys[i].GetPosition()).magnitude;
                if (distance < currDistance || closestPrey == null)
                {
                    currDistance = distance;
                    closestPrey = preys[i];
                }
            }
        }

        return closestPrey;
    }
}
