using UnityEngine;

public class SeekBehaviour : Steering
{
    public Vector3 DoSeek(ICharacterInfo owner, Vector3 target)
    {
        steeringForce = Vector3.zero;

        Vector3 desiredVelocity = (target - owner.GetPosition()).normalized * owner.GetMaxSpeed();
        steeringForce = desiredVelocity - owner.GetVelocity();
        return steeringForce;
    }
}