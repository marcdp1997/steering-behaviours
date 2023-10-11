using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] protected bool drawGizmos;
    //[SerializeField] protected int priority;

    protected Vector3 steeringForce;

    protected virtual void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + steeringForce);
    }
}
