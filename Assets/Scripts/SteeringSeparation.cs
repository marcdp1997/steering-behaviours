using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour
{
    private Move scrMove;

    private float force;
    private Vector3 distance;
    private Vector3 steeringForce;

    public float searchRadius = 2.0f;
    public float maxRepulsion = 0.5f;

    void Awake()
    {
        scrMove = GetComponent<Move>();
    }

    void FixedUpdate()
    {
        // Detecting all agent colliders with x layer that enter this sphere.
        int layerId = 8;
        int layerMask = 1 << layerId;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, layerMask);

        // Calculating force depending on distance between agents (more dist, less force)
        // If agent collides with another agent that has arrived at destination, 
        // he waits because he can't reach the target
        steeringForce = Vector3.zero;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!hitColliders[i].gameObject.GetComponent<SteeringArrive>().GetArrived())
            {
                distance = hitColliders[i].transform.position - transform.position;

                if (distance.magnitude > 0) force = maxRepulsion / distance.magnitude;
                else force = maxRepulsion;

                steeringForce += -distance.normalized * force;
            }
        }

        // Cap steering Force
        if (steeringForce.magnitude > maxRepulsion)
        {
            steeringForce = steeringForce.normalized * maxRepulsion;
        }

        // Apply force
        scrMove.AddVelocity(steeringForce);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
