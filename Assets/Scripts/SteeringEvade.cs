using UnityEngine;
using UnityEditor;

public class SteeringEvade : SteeringBehaviour
{
    private Vector3 targetFuturePos;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos || targetFuturePos == Vector3.zero) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(targetFuturePos, transform.up, 0.25f);
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        Vector3 distance = transform.position - targetPosition;
        float futureAhead = distance.magnitude / aiController.GetMaxSpeed();
        targetFuturePos = targetPosition + aiController.GetTarget().GetVelocity() * futureAhead;

        Vector3 desiredVelocity = (transform.position - targetPosition).normalized * aiController.GetMaxSpeed();
        steeringForce = desiredVelocity - aiController.GetVelocity();
    }
}
