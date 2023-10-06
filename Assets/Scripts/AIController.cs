﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class AIController : MonoBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes 

    [SerializeField] private Transform target;
    [SerializeField] private float maxSpeed = 8.0f;
    [SerializeField] private float maxSteeringForce = 0.2f;
    [SerializeField] private float maxRotation = 2.0f;
    [SerializeField] private float stopAngle = 0.2f;
    [SerializeField] private float slowAngle = 30.0f;
    [SerializeField] private float timeRotating = 0.1f;

    private Rigidbody rb;
    private List<SteeringBehaviour> steerings;
    private float rotation;
    private Vector3 steeringForceSum;

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        steerings = new List<SteeringBehaviour>();
    }

    private void Start()
    {
        GetSteeringBehaviors();
    }

    private void FixedUpdate()
    {
        UpdateSteeringBehaviours();
        ApplySteeringBehaviours();
        ApplyRotation();
    }

    private void OnDrawGizmos()
    {
        if (rb == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rb.velocity);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Methods

    private void GetSteeringBehaviors()
    {
        SteeringBehaviour[] steeringBehaviors = GetComponents<SteeringBehaviour>();

        for (int i = 0; i < steeringBehaviors.Length; i++)
            steerings.Add(steeringBehaviors[i]);
    }

    private void UpdateSteeringBehaviours()
    {
        for (int i = 0; i < steerings.Count; i++)
            steerings[i].UpdateSteeringBehavior();     
    }

    private void ApplySteeringBehaviours()
    {
        // Apply a steering force to the AI, in the direction of the desired acceleration,
        // truncated to the maximum allowed force.
        steeringForceSum = Vector3.zero;
        int currPriority = 0;

        for (int i = 0; i < steerings.Count; i++)
        {
            if (steerings[i].GetPriority() > currPriority)
            {
                steeringForceSum = Vector3.zero;
                currPriority = steerings[i].GetPriority();
            }

            if (steerings[i].GetPriority() == currPriority)
                steeringForceSum += steerings[i].GetSteeringForce();
        }
    
        steeringForceSum.y = 0;
        steeringForceSum = Vector3.ClampMagnitude(steeringForceSum, maxSteeringForce);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity + steeringForceSum, maxSpeed);
    }

    private void ApplyRotation()
    {
        if (rb.velocity == Vector3.zero) return;

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

    public Transform GetTarget() { return target; }

    public float GetMaxSpeed() { return maxSpeed; }

    public Vector3 GetVelocity() { return rb.velocity; }

    #endregion
    // -----------------------------------------------------------------------------------
}
