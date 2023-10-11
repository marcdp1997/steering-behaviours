using UnityEngine;
using UnityEditor;
using System.Linq;

public class QueueBehaviour : Steering
{
    [SerializeField] private float maxAhead = 4.0f;
    [SerializeField] private float maxRadius = 2.0f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Vector3 circleCenter = transform.position + transform.forward * maxAhead;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(circleCenter, transform.up, maxRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, circleCenter);
    }

    public Vector3 DoQueue(ICharacterInfo owner, Vector3 steeringForceSum)
    {
        // If the distance between that point and a neighbour character
        // is less than or equal to maxRadius, it means there
        // is someone ahead and the character must stop moving.
        steeringForce = Vector3.zero;
        Vector3 circleCenter = owner.GetPosition() + owner.GetForward() * maxAhead;

        Collider[] hitColliders = Physics.OverlapSphere(circleCenter, maxRadius, 1 << LayerMask.NameToLayer("AI"));
        Collider[] hitCollidersOrdered = hitColliders.OrderBy(c => (circleCenter - c.transform.position).sqrMagnitude).ToArray();

        if (hitCollidersOrdered.Length > 0)
        {
            steeringForce = (-steeringForceSum * 0.8f) + (-owner.GetVelocity());
        }

        return steeringForce;
    }
}
