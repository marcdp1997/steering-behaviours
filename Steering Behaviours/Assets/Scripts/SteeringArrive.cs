using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour 
{
    public float stop_area_radius = 0.5f;
    public float slow_area_radius = 2.0f;

    private Move move;

	// Use this for initialization
	void Start()
    {
        move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector3 desired_velocity = move.target.transform.position - transform.position;
        float distance = desired_velocity.magnitude;

        if (distance < stop_area_radius)
        {
            move.SetVelocity(Vector3.zero);
        }
        else
        {
            if (distance < slow_area_radius) desired_velocity = desired_velocity.normalized * move.max_velocity * (distance / slow_area_radius);
            else desired_velocity = desired_velocity.normalized * move.max_velocity;

            Vector3 steering_force = desired_velocity - move.GetVelocity();
            steering_force.y = 0.0f;

            move.AddSteeringForce(steering_force);
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(move.target.transform.position, slow_area_radius);
        //Gizmos.DrawWireSphere(move.target.transform.position, stop_area_radius);
    }
}