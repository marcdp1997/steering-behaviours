using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour
{
    private Move scrMove;

    private float force;
    private Vector3 vDistance;
    private Vector3 vSteeringForce;

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
        vSteeringForce = Vector3.zero;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!hitColliders[i].gameObject.GetComponent<SteeringArrive>().arrived)
            {
                vDistance = hitColliders[i].transform.position - transform.position;

                if (vDistance.magnitude > 0) force = maxRepulsion / vDistance.magnitude;
                else force = maxRepulsion;

                vSteeringForce += -vDistance.normalized * force;
            }
        }

        // Cap steering Force
        if (vSteeringForce.magnitude > maxRepulsion)
        {
            vSteeringForce = vSteeringForce.normalized * maxRepulsion;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
