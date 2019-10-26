using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    [Header("------ Read Only -------")]
    public Vector3 velocity = Vector3.zero;
    public float rotation = 0.0f;

    [Header("------ Set Values ------")]
    public GameObject target;
    public float max_velocity = 3.0f;
    public float max_rotation = 2.0f;
    public float max_acceleration = 0.1f;

    //public bool doing_queue = false;

    public void AddVelocity(Vector3 steering_force)
    {
        velocity += steering_force;
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
        // Cap velocity
        if (velocity.magnitude > max_velocity)
        {
            velocity = velocity.normalized * max_velocity;
        }

        // Move
        velocity.y = 0.0f;
        transform.position += velocity * Time.deltaTime;

        // Rotate
        transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime, -max_rotation, max_rotation), Vector3.up);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (velocity));
    }
}
