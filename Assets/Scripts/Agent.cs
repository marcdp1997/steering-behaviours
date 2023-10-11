using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour, ICharacterInfo
{
    // -----------------------------------------------------------------------------------
    #region Attributes 

    [Header("Agent")]
    [SerializeField] private float maxSpeed = 8.0f;
    [SerializeField] private float maxSteeringForce = 0.2f;
    [SerializeField] private float maxRotation = 2.0f;
    [SerializeField] private float stopAngle = 0.2f;
    [SerializeField] private float slowAngle = 30.0f;
    [SerializeField] private float timeRotating = 0.1f;
    [SerializeField] protected SteeringController steering;

    protected Rigidbody rb;
    protected float rotation;
    protected Vector3 steeringForceSum;

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        steering.SetOwner(this);
    }

    private void FixedUpdate()
    {
        ApplySteeringBehaviours();
        ApplyRotation();
    }

    private void OnDrawGizmos()
    {
        if (rb == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Methods

    private void ApplySteeringBehaviours()
    {
        // Apply a steering force to the AI, in the direction of the desired acceleration,
        // truncated to the maximum allowed force.
        steeringForceSum = Vector3.zero;
        CalculateSteeringSum();
        steeringForceSum.y = 0;
        steeringForceSum = Vector3.ClampMagnitude(steeringForceSum, maxSteeringForce);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity + steeringForceSum, maxSpeed);
    }

    protected virtual void CalculateSteeringSum() { }

    private void ApplyRotation()
    {
        if (rb.velocity.magnitude <= 0.2f) return;

        float myOrientation = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float facingOrientation = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
        float diff = Mathf.DeltaAngle(myOrientation, facingOrientation);

        if (Mathf.Abs(diff) - maxRotation < stopAngle)
        {
            rotation = 0.0f;
        }
        else
        {
            float idealRotation = Mathf.Abs(diff) > slowAngle ? maxRotation : maxRotation * (Mathf.Abs(diff) / slowAngle);
            idealRotation /= timeRotating;
            if (diff < 0) idealRotation *= -1.0f;

            rotation += idealRotation;
        }

        // Rotate
        transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime,
            -maxRotation, maxRotation), Vector3.up);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Getters & Setters

    public Vector3 GetPosition() { return transform.position; }

    public float GetMaxSpeed() { return maxSpeed; }

    public Vector3 GetVelocity() { return rb.velocity; }

    public Vector3 GetForward() { return transform.forward; }

    #endregion
    // -----------------------------------------------------------------------------------
}
