using UnityEngine;

public class SteeringSeek : SteeringBehaviour
{
    #region Steering  
    public override void PerformSteeringBehavior()
    {
        Vector3 desiredVelocity = (move.target.transform.position - transform.position).normalized * move.maxSpeed;
        steeringForce = desiredVelocity - move.velocity;
    }

    #endregion
}