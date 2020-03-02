using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour
{
    private Move scrMove;

    private float force;
    private Vector3 distance;
    private Vector3 steeringForce;

    public float searchRadius;
    public float maxRepulsion;

    void Awake()
    {
        scrMove = GetComponent<Move>();
    }

    void FixedUpdate()
    {
        int layerId = 8;
        int layerMask = 1 << layerId;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, layerMask);

        steeringForce = Vector3.zero;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            distance = hitColliders[i].transform.position - transform.position;

            if (distance.magnitude > 0) force = maxRepulsion / distance.magnitude;
            else force = maxRepulsion;

            steeringForce += -distance.normalized * force;
        }

        if (steeringForce.magnitude > maxRepulsion)
        {
            steeringForce = steeringForce.normalized * maxRepulsion;
        }

        scrMove.AddVelocity(steeringForce);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
