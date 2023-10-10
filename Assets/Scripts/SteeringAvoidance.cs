using UnityEngine;
using System;
using System.Collections.Generic;

public class SteeringAvoidance : SteeringBehaviour
{
    [Serializable] 
    public struct Ray
    {
        public float angleOffset;
        public float length;
    }

    [SerializeField] private float maxAvoidanceForce = 5.0f;
    [SerializeField] private List<Ray> rays;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        for (int i = 0; i < rays.Count; i++)
        {
            Gizmos.color = Color.cyan;

            float dynamicLength = rays[i].length;
            if (aiController != null)
                dynamicLength *= aiController.GetVelocity().magnitude / aiController.GetMaxSpeed();

            Vector3 direction = Quaternion.AngleAxis(rays[i].angleOffset, Vector3.up) * transform.forward * dynamicLength;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        for (int i = 0; i < rays.Count; i++)
        {
            float dynamicLength = rays[i].length * (aiController.GetVelocity().magnitude / aiController.GetMaxSpeed());
            Vector3 direction = Quaternion.AngleAxis(rays[i].angleOffset, Vector3.up) * transform.forward * dynamicLength;
            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, 1 << LayerMask.NameToLayer("Obstacle"));
            int mostThreatening = FindMostThreatening(hits, rays[i].length);

            if (hits.Length > 0)
            {
                steeringForce = (transform.position + direction) - hits[mostThreatening].collider.transform.position;
                steeringForce = steeringForce.normalized * maxAvoidanceForce;
                break;
            }
        }
    }

    private int FindMostThreatening(RaycastHit[] hits, float rayLength)
    {
        int mostThreatening = 0;
        float shortestDistance = rayLength;

        for (int j = 0; j < hits.Length; j++)
        {
            float distance = (transform.position - hits[j].transform.position).magnitude;
            if (distance < shortestDistance) mostThreatening = j;
        }

        return mostThreatening;
    }
}