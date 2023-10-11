using UnityEngine;
using UnityEditor;

public class SeparationBehaviour : Steering
{
    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float maxSeparationForce = 5.0f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.up, radius);
    }

    public Vector3 DoSeparation(ICharacterInfo owner)
    {
        steeringForce = Vector3.zero;
        Collider[] hitColliders = Physics.OverlapSphere(owner.GetPosition(), radius, 1 << LayerMask.NameToLayer("AI"));

        for (int i = 0; i < hitColliders.Length; i++)
            steeringForce += owner.GetPosition() - hitColliders[i].transform.position;

        if (hitColliders.Length > 0)
            steeringForce = steeringForce.normalized * maxSeparationForce;

        return steeringForce;
    }
}
