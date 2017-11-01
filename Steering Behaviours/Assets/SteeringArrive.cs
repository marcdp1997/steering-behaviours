using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour {

    public float max_force;

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
        desired_velocity = desired_velocity.normalized * move.max_velocity;

        Vector3 steering = desired_velocity - move.GetVelocity();
        steering = steering.normalized * max_force;

        move.AddSteeringForce(steering);
    }
}
