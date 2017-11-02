using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlign : MonoBehaviour {

    Move move;
    public float min_rotation = 5.0f;

	// Use this for initialization
	void Start ()
    {
        move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float my_orientation = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float velocity_orientation = Mathf.Atan2(move.GetVelocity().x, move.GetVelocity().z) * Mathf.Rad2Deg;
        float diff = Mathf.DeltaAngle(my_orientation, velocity_orientation);

        float absolute_diff = Mathf.Abs(diff);

        if (absolute_diff < min_rotation) move.SetRotation(0.0f);
        else move.AddRotation(Mathf.Clamp(diff, -move.max_rotation, move.max_rotation));
    }
}
