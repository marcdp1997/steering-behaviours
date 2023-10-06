using UnityEngine;

public class SteeringFlee : SteeringBehaviour
{
    [SerializeField] private float maxDistance = 5.0f;

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        if (Vector3.Distance(transform.position, aiController.GetTarget().position) < maxDistance)
        {
            Vector3 desiredVelocity = (transform.position - aiController.GetTarget().position).normalized * aiController.GetMaxSpeed();
            steeringForce = desiredVelocity - aiController.GetVelocity();
        }
    }
}
