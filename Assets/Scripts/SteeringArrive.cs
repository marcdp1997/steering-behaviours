using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour 
{
    private Move scrMove;

    private float slowFactor;
    private Vector3 distance;
    private Vector3 desiredVelocity;
    private Vector3 desiredAccel;
    private Vector3 steeringForce;

    [Header("------ Read Only ------")]    
    [SerializeField] private float distanceMagnitude = 0;

    [Header("------ Set Values ------")]
    public float stopRadius = 0.3f;
    public float slowRadius = 2.0f;
    public float timeAccelerating = 50.0f;

	void Awake()
    {
        scrMove = GetComponent<Move>();
	}
	
	void FixedUpdate()
    {
        distance = scrMove.target.transform.position - transform.position;
        distanceMagnitude = distance.magnitude;

        slowFactor = distanceMagnitude / slowRadius;

        if (distanceMagnitude > stopRadius)
        { 
            // Finding desired velocity
            desiredVelocity = distance.normalized * scrMove.maxVelocity;

            // Finding desired deceleration
            // To decelerate we only use the distance.
            if (distanceMagnitude <= slowRadius)
                desiredVelocity *= slowFactor;

            // Finding desired acceleration
            // To accelerate we divide by the time we want the object to be accelerated. 
            desiredAccel = desiredVelocity - scrMove.GetVelocity();

            if (distanceMagnitude >= slowRadius)
                desiredAccel /= timeAccelerating;

            // Cap desired acceleration
            if (desiredAccel.magnitude >= scrMove.maxAcceleration)
                desiredAccel = desiredAccel.normalized * scrMove.maxAcceleration;

            // Add steering force
            steeringForce = desiredAccel;
            scrMove.AddVelocity(steeringForce);
        }
        else
        {
            // Path is completed
            scrMove.SetVelocity(Vector3.zero);
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(scrMove.target.transform.position, slowRadius);
        //Gizmos.DrawWireSphere(scrMove.target.transform.position, stopRadius);
    }
}