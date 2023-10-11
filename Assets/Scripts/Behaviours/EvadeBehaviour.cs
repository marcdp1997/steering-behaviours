using UnityEngine;
using UnityEditor;

public class EvadeBehaviour : Steering
{
    private Vector3 targetFuturePos;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(targetFuturePos, Vector3.up, 0.25f);
    }

    public Vector3 DoEvade(ICharacterInfo owner, ICharacterInfo target)
    {
        steeringForce = Vector3.zero;

        Vector3 distance = owner.GetPosition() - target.GetPosition();
        float futureAhead = distance.magnitude / owner.GetMaxSpeed();
        targetFuturePos = target.GetPosition() + target.GetVelocity() * futureAhead;

        Vector3 desiredVelocity = distance.normalized * owner.GetMaxSpeed();
        steeringForce = desiredVelocity - owner.GetVelocity();
        return steeringForce;
    }
}
