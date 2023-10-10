using UnityEngine;
using UnityEditor;

public class SteeringArrive : SteeringBehaviour
{
    [SerializeField] private float stopRadius = 2.0f;
    [SerializeField] private float slowRadius = 5.0f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos || aiController == null) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(targetPosition, transform.up, slowRadius);
        Handles.DrawWireDisc(targetPosition, transform.up, stopRadius);
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        Vector3 desiredVelocity = targetPosition - transform.position;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * aiController.GetMaxSpeed();

        if (distance <= slowRadius)
        {
            desiredVelocity *= ((distance - stopRadius) / (slowRadius - stopRadius));
        }

        steeringForce = desiredVelocity - aiController.GetVelocity();
    }
}
