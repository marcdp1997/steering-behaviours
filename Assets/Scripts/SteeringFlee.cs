using UnityEngine;

public class SteeringFlee : SteeringBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes 

    [SerializeField] private float maxDistance = 5.0f;

    #endregion
    // -----------------------------------------------------------------------------------
    #region Steering  

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        if (Vector3.Distance(transform.position, aiController.GetTarget().position) < maxDistance)
        {
            Vector3 desiredVelocity = (transform.position - aiController.GetTarget().position).normalized * aiController.GetMaxSpeed();
            steeringForce = desiredVelocity - aiController.GetVelocity();
        }
    }

    #endregion
    // -----------------------------------------------------------------------------------
}
