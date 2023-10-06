using UnityEngine;
using UnityEditor;

public class SteeringSeek : SteeringBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes 

    [SerializeField] private float stopRadius = 2.0f;
    [SerializeField] private float slowRadius = 5.0f;

    private const float StopOffset = 0.5f;

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour

    private void OnDrawGizmos()
    {
        if (aiController == null) return;

        Handles.color = Color.white;
        Handles.DrawWireDisc(aiController.GetTarget().position, transform.up, slowRadius);
        Handles.DrawWireDisc(aiController.GetTarget().position, transform.up, stopRadius);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Steering  

    public override void UpdateSteeringBehavior()
    {
        base.UpdateSteeringBehavior();

        Vector3 desiredVelocity = aiController.GetTarget().position - transform.position;
        float distance = desiredVelocity.magnitude;
        float slowFactor = 1;

        if (distance <= slowRadius)
        {
            slowFactor = (distance - stopRadius) / (slowRadius - stopRadius); 
        }

        if (distance <= stopRadius + StopOffset)
        {
            slowFactor = 0;
        }

        desiredVelocity = aiController.GetMaxSpeed() * slowFactor * desiredVelocity.normalized;
        steeringForce = desiredVelocity - aiController.GetVelocity();
    }

    #endregion
    // -----------------------------------------------------------------------------------
}