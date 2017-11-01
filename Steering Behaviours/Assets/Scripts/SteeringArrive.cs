using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour {

    public float max_force;
    public float stop_area_radius;
    public float slow_area_radius;

    private Move move;

	// Use this for initialization
	void Start ()
    {
        move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Steer(move.target.transform.position);
	}

    public void Steer(Vector3 target_position)
    {
        Vector3 desired_velocity = target_position - transform.position;
        float distance = desired_velocity.magnitude;

        if (distance < slow_area_radius)
            desired_velocity = desired_velocity.normalized * move.max_velocity * (distance / slow_area_radius);
        else
            desired_velocity = desired_velocity.normalized * move.max_velocity;

        Vector3 steering = desired_velocity - move.GetVelocity();
        steering = steering.normalized * max_force;

        if (distance < stop_area_radius) move.SetVelocity(Vector3.zero);
        else move.AddSteeringForce(steering);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(move.target.transform.position, slow_area_radius);
        Gizmos.DrawWireSphere(move.target.transform.position, stop_area_radius);
    }
}
