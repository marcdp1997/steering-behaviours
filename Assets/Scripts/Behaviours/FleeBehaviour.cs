using UnityEngine;
using UnityEditor;

public class FleeBehaviour : Steering
{
    [SerializeField] private float maxDistance = 5.0f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, Vector3.up, maxDistance);
    }

    public Vector3 DoFlee(ICharacterInfo owner, Vector3 target)
    {
        steeringForce = Vector3.zero;

        if (Vector3.Distance(owner.GetPosition(), target) < maxDistance)
        {
            Vector3 desiredVelocity = (owner.GetPosition() - target).normalized * owner.GetMaxSpeed();
            steeringForce = desiredVelocity - owner.GetVelocity();
        }

        return steeringForce;
    }
}
