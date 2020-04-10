using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes 
    // -----------------------------------------------------------------------------------

    private Rigidbody rb = null;
    private List<SteeringBehaviour> steerings = null;
    private float velocityMagnitude = 0.0f;
    private float rotation = 0.0f;
    private Vector3 steeringForceSum = Vector3.zero;

    public Text debugText = null;
    public GameObject target = null;
    public float maxSpeed = 8.0f;
    public float maxSteering = 0.2f;
    public float maxRotation = 2.0f;
    [Range(0, 5)] public float stopAngle = 0.2f;
    [Range(0, 50)] public float slowAngle = 30.0f;
    public float timeRotating = 0.1f;

    #endregion
    // -----------------------------------------------------------------------------------
    #region Getters and setters 
    // -----------------------------------------------------------------------------------

    public Vector3 velocity
    {
        get { return rb.velocity; }
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour
    // -----------------------------------------------------------------------------------

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
        UpdateSteering();
        ApplySteering();
        ApplyRotation();
        DrawDebugText();
    }

    private void OnDrawGizmos()
    {
        if (rb == null)
        {
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + steeringForceSum);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Private manipulators 
    // -----------------------------------------------------------------------------------

    private void GetSteeringBehaviors()
    {
        SteeringBehaviour[] steeringBehaviors = GetComponents<SteeringBehaviour>();

        for (int i = 0; i < steeringBehaviors.Length; i++)
        {
            steerings.Add(steeringBehaviors[i]);
        }
    }

    private void UpdateSteering()
    {
        for (int i = 0; i < steerings.Count; i++)
        {
            if (steerings[i].enabled)
            {
                steerings[i].PerformSteeringBehavior();
            }
        }
    }

    private void ApplySteering()
    {
        // Get steering force average
        steeringForceSum = Vector3.zero;
        float priorityScale = 1;

        for (int i = 0; i < steerings.Count; i++)
        {
            if (steerings.Count > 1)
            {
                priorityScale = steerings[i].priority;
            }

            steeringForceSum += steerings[i].steeringForce * priorityScale;
        }

        // Cap velocity
        steeringForceSum.y = 0;
        rb.velocity += Vector3.ClampMagnitude(steeringForceSum, maxSteering);
        velocityMagnitude = rb.velocity.magnitude;
    }

    private void ApplyRotation()
    {
        // If the agent is in stop radius we need to stop movement BUT keep
        // facing target's position
        Vector3 desiredDirection;
        if (velocity != Vector3.zero) desiredDirection = velocity;
        else desiredDirection = target.transform.position - transform.position;

        float myOrientation = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float facingOrientation = Mathf.Atan2(desiredDirection.x, desiredDirection.z) * Mathf.Rad2Deg;
        float diff = Mathf.DeltaAngle(myOrientation, facingOrientation);

        if (Mathf.Abs(diff) - maxRotation < stopAngle)
        {
            rotation = 0.0f;
        }
        else
        {
            float idealRotation;
            if (Mathf.Abs(diff) > slowAngle)
            {
                idealRotation = maxRotation;
            }
            else
            {
                idealRotation = maxRotation * (Mathf.Abs(diff) / slowAngle);
            }

            idealRotation = idealRotation / timeRotating;
            if (diff < 0) idealRotation *= -1.0f;

            rotation += idealRotation;
        }

        // Rotate
        transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime,
            -maxRotation, maxRotation), Vector3.up);
    }

    private void DrawDebugText()
    {
        debugText.text = "Velocity: " + velocity.ToString("F2") + "\n" +
                         "Magnitude: " + velocityMagnitude.ToString("F2") + "\n" +
                         "Rotation: " + rotation.ToString("F2") + "\n";
    }

    #endregion
    // -----------------------------------------------------------------------------------
}
