using UnityEngine;
using System;
using System.Collections.Generic;

public class AvoidanceBehaviour : Steering
{
    [Serializable] 
    public struct Ray
    {
        public float angleOffset;
        public float length;
    }

    [SerializeField] private float maxAvoidanceForce = 5.0f;
    [SerializeField] private List<Ray> rays;

    private ICharacterInfo owner;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        for (int i = 0; i < rays.Count; i++)
        {
            float dynamicLength = rays[i].length;
            if (owner != null) dynamicLength *= owner.GetVelocity().magnitude / owner.GetMaxSpeed();
            Vector3 direction = Quaternion.AngleAxis(rays[i].angleOffset, Vector3.up) * transform.forward * dynamicLength;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }
    }

    public Vector3 DoAvoidance(ICharacterInfo owner)
    {
        //Gizmo
        this.owner = owner;

        //Steering
        steeringForce = Vector3.zero;

        for (int i = 0; i < rays.Count; i++)
        {
            float dynamicLength = rays[i].length * (owner.GetVelocity().magnitude / owner.GetMaxSpeed());
            Vector3 direction = Quaternion.AngleAxis(rays[i].angleOffset, Vector3.up) * owner.GetVelocity().normalized * dynamicLength;
            RaycastHit[] hits = Physics.RaycastAll(owner.GetPosition(), direction, direction.magnitude, 1 << LayerMask.NameToLayer("Obstacle"));
            int mostThreatening = FindMostThreatening(hits, rays[i].length);

            if (hits.Length > 0)
            {
                steeringForce = (owner.GetPosition() + direction) - hits[mostThreatening].collider.transform.position;
                steeringForce = steeringForce.normalized * maxAvoidanceForce;
                break;
            }
        }

        return steeringForce;
    }

    private int FindMostThreatening(RaycastHit[] hits, float rayLength)
    {
        int mostThreatening = 0;
        float shortestDistance = rayLength;

        for (int j = 0; j < hits.Length; j++)
        {
            float distance = (owner.GetPosition() - hits[j].transform.position).magnitude;
            if (distance < shortestDistance) mostThreatening = j;
        }

        return mostThreatening;
    }
}