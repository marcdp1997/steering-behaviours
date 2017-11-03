using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour 
{
	private Move move;

	public float max_see_ahead = 1.5f; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
	public float max_avoid_force = 10.0f;

    Vector3 avoidance_force = Vector3.zero;
    Vector3 central_ray = Vector3.zero;

    // Use this for initialization
    void Start() 
	{
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		RaycastHit hit;

        // Creating vectors that will detect the collision (central with two short whiskers).
        // Well, first trying with one vector xD
        avoidance_force = Vector3.zero;
        central_ray = Vector3.zero;

        central_ray = transform.position + (move.GetVelocity().normalized * max_see_ahead);

        if (Physics.Raycast(transform.position, central_ray.normalized, out hit, max_see_ahead))
        {
            avoidance_force = hit.normal.normalized * max_avoid_force;
            avoidance_force.y = 0.0f;
            move.AddSteeringForce(avoidance_force);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + central_ray);
    }
}
