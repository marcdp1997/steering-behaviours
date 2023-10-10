using UnityEngine;
using UnityEditor;

public class SteeringWander : SteeringBehaviour
{
    [SerializeField] private float circleDistance = 7.0f;
    [SerializeField] private float wanderForce = 4.0f;
    [SerializeField] private float maxAngle = 45.0f;

    private float currAngle;
    private Vector3 displacement;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Vector3 circleCenter = transform.position + transform.forward * circleDistance;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(circleCenter, transform.up, wanderForce);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, circleCenter);

        if (displacement == Vector3.zero) return;

        Gizmos.DrawLine(circleCenter, circleCenter + displacement);
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        Vector3 ahead = transform.forward * circleDistance;
        displacement = Quaternion.AngleAxis(currAngle, Vector3.up) * (Vector3.forward * wanderForce);
        currAngle += (Random.value * maxAngle) - (maxAngle * 0.5f);

        steeringForce = ahead + displacement;
    }
}
