using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlign : MonoBehaviour {

    Move move;
    public float stop_angle = 5.0f;
	public float slow_angle = 20.0f;
	public float time_rotating = 0.1f;

	// Use this for initialization
	void Start()
    {
        move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update()
    {
        float my_orientation = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float velocity_orientation = Mathf.Atan2(move.GetVelocity().x, move.GetVelocity().z) * Mathf.Rad2Deg;
        float diff = Mathf.DeltaAngle(my_orientation, velocity_orientation);

		if(Mathf.Abs(diff) < stop_angle || move.GetVelocity() == Vector3.zero) 
		{
			move.SetRotation(0.0f);
		} 
		else 
		{
			float ideal_rotation = 0.0f;

			if(Mathf.Abs(diff) > slow_angle) ideal_rotation = move.max_rotation;
			else ideal_rotation = move.max_rotation * (diff / slow_angle);

			if(diff < 0) ideal_rotation *= -1.0f;

			move.AddRotation(ideal_rotation / time_rotating);
		}
    }

	void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
	}
}
