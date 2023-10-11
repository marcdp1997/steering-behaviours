using UnityEngine;
using UnityEditor;

public class ArriveBehaviour : Steering
{
    [SerializeField] private float stopRadius = 2.0f;
    [SerializeField] private float slowRadius = 5.0f;

    private Vector3 target;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos || !Application.isPlaying) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(target, transform.up, slowRadius);
        Handles.DrawWireDisc(target, transform.up, stopRadius);
    }

    public Vector3 DoArrive(ICharacterInfo owner, Vector3 target)
    {
        //Gizmo
        this.target = target;

        //Steering
        steeringForce = Vector3.zero;

        Vector3 desiredVelocity = target - owner.GetPosition();
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * owner.GetMaxSpeed();

        if (distance <= slowRadius)
        {
            desiredVelocity *= ((distance - stopRadius) / (slowRadius - stopRadius));
        }

        steeringForce = desiredVelocity - owner.GetVelocity();
        return steeringForce;
    }
}
