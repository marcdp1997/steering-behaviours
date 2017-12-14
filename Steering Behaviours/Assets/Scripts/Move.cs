﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    public GameObject target;
    public float max_velocity = 3.0f;
    public float max_rotation = 2.0f;
    public float max_force = 2.0f;
    public bool doing_queue = false;

    private Vector3 velocity = Vector3.zero;
    private Vector3 steering = Vector3.zero;
    private float rotation = 0.0f;

    public void AddSteeringForce(Vector3 force)
    {
        steering += force;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void SetVelocity(Vector3 new_velocity)
    {
        velocity = new_velocity;
    }

    public void AddRotation(float new_rotation)
    {
        rotation += new_rotation;
    }

    public void SetRotation(float new_rotation)
    {
        rotation = new_rotation;
    }

    // Use this for initialization
    void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        if (!doing_queue)
        {
            // Cap steering force
            if (steering.magnitude > max_force)
            {
                steering = steering.normalized * max_force;
            }

            // Adding force to velocity
            velocity += steering;
            steering = Vector3.zero;

            // Cap velocity
            if (velocity.magnitude > max_velocity)
            {
                velocity = velocity.normalized * max_velocity;
            }

            // Move
            transform.position += velocity * Time.deltaTime;

            // Rotate
            transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime, -max_rotation, max_rotation), Vector3.up);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (velocity));
    }
}
