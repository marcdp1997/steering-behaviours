using UnityEngine;
using UnityEditor;

public class SteeringFlee : SteeringBehaviour
{
    [SerializeField] private float maxDistance = 5.0f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.up, maxDistance);
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        if (Vector3.Distance(transform.position, targetPosition) < maxDistance)
        {
            Vector3 desiredVelocity = (transform.position - targetPosition).normalized * aiController.GetMaxSpeed();
            steeringForce = desiredVelocity - aiController.GetVelocity();
        }
    }
}
