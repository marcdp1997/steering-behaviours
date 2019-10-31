using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour 
{
    private Move move;
    private MoveNavMesh nav_move;
    private SteeringQueue queue;

    [Header("------ Read Only ------")]
    public bool arrived = false;
    public float distance = 0;

    [Header("------ Set Values ------")]
    public float stop_area_radius = 0.3f;
    public float slow_area_radius = 2.0f;
    public float time_to_target = 0.1f;

	// Use this for initialization
	void Start()
    {
        move = GetComponent<Move>();
        nav_move = GetComponent<MoveNavMesh>();
        queue = GetComponent<SteeringQueue>();
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector3 dist = move.target.transform.position - transform.position;
        distance = dist.magnitude;
        float slow_factor = dist.magnitude / slow_area_radius;

        if (!queue.is_in_queue)
        {
            if (dist.magnitude <= stop_area_radius)
            {
                // Path is completed
                move.SetVelocity(Vector3.zero);
                arrived = true;
            }
            else
            {           
                // Finding desired velocity
                Vector3 desired_velocity = dist.normalized * move.max_velocity;

                // Finding desired deceleration
                // To decelerate we only use the distance.
                if (dist.magnitude <= slow_area_radius && nav_move.IsPathFinishing())
                    desired_velocity *= slow_factor;

                // Finding desired acceleration
                // To accelerate we divide by the time we want the object to be accelerated. 
                Vector3 desired_accel = (desired_velocity - move.velocity);

                if (dist.magnitude >= slow_area_radius)
                    desired_accel /= time_to_target;

                // Cap desired acceleration
                if (desired_accel.magnitude >= move.max_acceleration)
                    desired_accel = desired_accel.normalized * move.max_acceleration;

                // Add steering force
                Vector3 steering_force = desired_accel;
                move.AddVelocity(steering_force);
            }        
        }     
    }

    void OnDrawGizmos()
    {
        if (nav_move.IsPathFinishing())
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(move.target.transform.position, slow_area_radius);
            Gizmos.DrawWireSphere(move.target.transform.position, stop_area_radius);
        }
    }
}