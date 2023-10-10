using UnityEngine;
using UnityEditor;
using System.Linq;

public class SteeringQueue : SteeringBehaviour
{
    [SerializeField] private float maxAhead = 4.0f;
    [SerializeField] private float maxRadius = 2.0f;

    public Vector3 prevSteeringSum;

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

    private void LateUpdate()
    {
        prevSteeringSum = aiController.GetSteeringSum();
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        // If the distance between that point and a neighbour character
        // is less than or equal to maxRadius, it means there
        // is someone ahead and the character must stop moving.
        Vector3 circleCenter = transform.position + transform.forward * maxAhead;

        Collider[] hitColliders = Physics.OverlapSphere(circleCenter, maxRadius, 1 << LayerMask.NameToLayer("AI"));
        Collider[] hitCollidersOrdered = hitColliders.OrderBy(c => (circleCenter - c.transform.position).sqrMagnitude).ToArray();

        if (hitColliders.Length > 0)
        {
            //brake
            steeringForce = -aiController.GetVelocity() * 0.9f;
        }
    }
}
