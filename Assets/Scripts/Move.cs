using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    private Rigidbody rb;

    [Header("------ Read Only -------")]
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float velocityMagnitude = 0;
    [SerializeField] private float rotation = 0.0f;
    [SerializeField] private bool waiting = false;

    [Header("------ Set Values ------")]
    public GameObject target;
    public float maxVelocity = 3.0f;
    public float maxRotation = 2.0f;
    public float maxAcceleration = 3.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Cap velocity
        if (velocityMagnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }

        // Disabling movement if agent is waiting
        if (waiting)
        {
            SetRotation(0.0f);
            SetVelocity(Vector3.zero);
        }

        // Move
        velocity.y = 0.0f;
        velocityMagnitude = velocity.magnitude;
        rb.velocity = velocity;

        // Rotate
        transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime, -maxRotation, maxRotation), Vector3.up);   
    }

    public void AddVelocity(Vector3 steeringForce)
    {
        velocity += steeringForce;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
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
        return velocity;
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
        Gizmos.DrawLine(transform.position, transform.position + velocity);
    }
}
