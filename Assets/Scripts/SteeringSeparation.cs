using UnityEngine;
using UnityEditor;

public class SteeringSeparation : SteeringBehaviour
{
    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float maxSeparationForce = 8.0f;
    [SerializeField] private LayerMask separationLayer;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (!drawGizmos) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.up, radius);
    }

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, separationLayer);

        // Calculating force depending on distance between agents (more dist, less force)
        // If agent collides with another agent that has arrived at destination, 
        // he waits because he can't reach the target
        for (int i = 0; i < hitColliders.Length; i++)
            steeringForce += transform.position - hitColliders[i].transform.position;

        if (hitColliders.Length > 0)
            steeringForce = steeringForce.normalized * maxSeparationForce;
    }
}
