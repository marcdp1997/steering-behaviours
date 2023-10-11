using UnityEngine;
using UnityEditor;

public class PursueBehaviour : Steering
{
    private Vector3 targetFuturePos;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos || !Application.isPlaying) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(targetFuturePos, Vector3.up, 0.25f);
    }

    public Vector3 DoPursuit(ICharacterInfo owner, ICharacterInfo target)
    {
        steeringForce = Vector3.zero;

        Vector3 distance = target.GetPosition() - owner.GetPosition();
        float futureAhead = distance.magnitude / target.GetMaxSpeed();
        targetFuturePos = target.GetPosition() + target.GetVelocity() * futureAhead;

        Vector3 desiredVelocity = (targetFuturePos - owner.GetPosition()).normalized * owner.GetMaxSpeed();
        steeringForce = desiredVelocity - owner.GetVelocity();
        return steeringForce;
    }
}
