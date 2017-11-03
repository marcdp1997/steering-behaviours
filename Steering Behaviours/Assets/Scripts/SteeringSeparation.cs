using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour 
{
	private Move move;

	public float max_see_ahead = 3.0f; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
	public float max_avoid_force = 5.0f;

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
		Vector3 central_ray = transform.position + (move.GetVelocity().normalized * max_see_ahead);
		Debug.DrawRay (transform.position, move.GetVelocity ().normalized * max_see_ahead);

		if (Physics.Raycast(transform.position, central_ray, out hit, central_ray.magnitude))
		{
			Vector3 avoidance_force = central_ray - hit.collider.transform.position;
			avoidance_force = avoidance_force.normalized * max_avoid_force;
			avoidance_force.y = 0.0f;
			move.AddSteeringForce(avoidance_force);
		}
	}
}
