using UnityEngine;

public class SteeringSeek : SteeringBehaviour
{
    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        Vector3 desiredVelocity = (targetPosition - transform.position).normalized * aiController.GetMaxSpeed();
        steeringForce = desiredVelocity - aiController.GetVelocity();
    }
}