using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    private Rigidbody rb;
    private Vector3 vTarget;

    [Header("------ Read Only -------")]
    [SerializeField] private Vector3 vVelocity = Vector3.zero;
    [SerializeField] private float velocityMagnitude = 0;
    [SerializeField] private float rotation = 0.0f;
    [SerializeField] private bool waiting = false;

    [Header("------ Set Values ------")]
    public GameObject initialTarget;
    public float maxVelocity = 3.0f;
    public float maxRotation = 2.0f;
    public float maxAcceleration = 3.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        vTarget = initialTarget.transform.position;
    }

    void FixedUpdate()
    {
        // Cap velocity
        if (velocityMagnitude > maxVelocity)
        {
            vVelocity = vVelocity.normalized * maxVelocity;
        }

        // Disabling movement if agent is waiting
        if (waiting)
        {
            SetRotation(0.0f);
            SetVelocity(Vector3.zero);
        }

        // Move
        vVelocity.y = 0.0f;
        velocityMagnitude = vVelocity.magnitude;
        rb.velocity = vVelocity;

        // Rotate
        transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime, -maxRotation, maxRotation), Vector3.up);   
    }

    public void SetTarget(Vector3 newTarget)
    {
        vTarget = newTarget;
    }

    public Vector3 GetTarget()
    {
        return vTarget;
    }

    public void AddVelocity(Vector3 steeringForce)
    {
        vVelocity += steeringForce;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        vVelocity = newVelocity;
    }

    public void AddRotation(float newRotation)
    {
        rotation += newRotation;
    }

    public void SetRotation(float newRotation)
    {
        rotation = newRotation;
    }

    public Vector3 GetVelocity()
    {
        return vVelocity;
    }

    public void SetWaiting(bool newWaiting)
    {
        waiting = newWaiting;
    }

    public bool GetWaiting()
    {
        return waiting;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + vVelocity);
    }
}
