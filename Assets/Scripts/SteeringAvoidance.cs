using UnityEngine;
using UnityEditor;

public class SteeringAvoidance : SteeringBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes 
    // -----------------------------------------------------------------------------------

    private Vector3 desiredVelocity = Vector3.zero;
    public LayerMask avoidanceLayer;

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour 
    // -----------------------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.up, 1.0f);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Steering
    // -----------------------------------------------------------------------------------

    public override void PerformSteeringBehavior()
    {
        Vector3 avoidanceForce = Vector3.zero;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1.0f, transform.forward, out hit, 10.0f, avoidanceLayer))
        {
            if (Vector3.Angle(hit.normal, transform.up) > 45.0f)
            {
                avoidanceForce = Vector3.Reflect(move.velocity, hit.normal);

                if (Vector3.Dot(avoidanceForce, move.velocity) < -0.9f)
                {
                    avoidanceForce = transform.right;
                }
            }
        }

        if (avoidanceForce != Vector3.zero)
        {
            desiredVelocity = avoidanceForce.normalized * move.maxSpeed;
            steeringForce = desiredVelocity - move.velocity;
        }
        else
        {
            steeringForce = Vector3.zero;
        }
    }

    #endregion
    // -----------------------------------------------------------------------------------
}