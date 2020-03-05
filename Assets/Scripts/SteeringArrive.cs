using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour 
{
    private Move scrMove;

    private float slowFactor;
    private Vector3 vDistance;
    private Vector3 vDesiredVelocity;
    private Vector3 vDesiredAccel;
    private Vector3 vSteeringForce;

    [Header("------ Read Only ------")]    
    [SerializeField] private float distanceMagnitude = 0;
    [SerializeField] private bool arrived = false;
    [SerializeField] private bool arriving = false;

    [Header("------ Set Values ------")]
    public float stopRadius = 0.6f;
    public float slowRadius = 2.0f;
    public float timeAccelerating = 50.0f;

	void Awake()
    {
        scrMove = GetComponent<Move>();
	}
	
	void FixedUpdate()
    {
        vDistance = scrMove.GetTarget() - transform.position;
        distanceMagnitude = vDistance.magnitude;

        slowFactor = distanceMagnitude / slowRadius;

        if (distanceMagnitude > stopRadius)
        {
            arrived = false;
            arriving = false;

            // Finding desired velocity
            vDesiredVelocity = vDistance.normalized * scrMove.maxVelocity;

            // Finding desired deceleration
            // To decelerate we only use the distance.
            if (distanceMagnitude <= slowRadius)
            {
                arriving = true;
                vDesiredVelocity *= slowFactor;
            }

            // Finding desired acceleration
            // To accelerate we divide by the time we want the object to be accelerated. 
            vDesiredAccel = vDesiredVelocity - scrMove.GetVelocity();

            if (distanceMagnitude >= slowRadius)
                vDesiredAccel /= timeAccelerating;

            // Cap desired acceleration
            if (vDesiredAccel.magnitude >= scrMove.maxAcceleration)
                vDesiredAccel = vDesiredAccel.normalized * scrMove.maxAcceleration;

            // Add steering force
            vSteeringForce = vDesiredAccel;
            scrMove.AddVelocity(vSteeringForce);
        }
        else
        {
            // Path is completed
            arrived = true;
            scrMove.SetVelocity(Vector3.zero);
        }
    }

    public bool GetArrived()
    {
        return arrived;
    }

    public bool GetArriving()
    {
        return arriving;
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(scrMove.goTarget.transform.position, slowRadius);
        //Gizmos.DrawWireSphere(scrMove.goTarget.transform.position, stopRadius);
    }
}