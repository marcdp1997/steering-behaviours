using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlign : MonoBehaviour
{
    private Move scrMove;

	private float myOrientation;
	private float velocityOrientation;
	private float diff;
	private float idealRotation;
	private Vector3 moveVelocity;

    public float stopAngle = 0.2f;
	public float slowAngle = 30.0f;
	public float timeRotating = 0.1f;

    void Awake()
    {
		scrMove = GetComponent<Move>();
	}
	
	void FixedUpdate()
    {
		moveVelocity = scrMove.GetVelocity();

        myOrientation = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        velocityOrientation = Mathf.Atan2(moveVelocity.x, moveVelocity.z) * Mathf.Rad2Deg;
        diff = Mathf.DeltaAngle(myOrientation, velocityOrientation);

		if(Mathf.Abs(diff) - scrMove.maxRotation < stopAngle || moveVelocity == Vector3.zero) 
		{
			scrMove.SetRotation(0.0f);
		} 
		else 
		{
			if(Mathf.Abs(diff) > slowAngle) idealRotation = scrMove.maxRotation;
			else idealRotation = scrMove.maxRotation * (Mathf.Abs(diff) / slowAngle);

			if(diff < 0) idealRotation *= -1.0f;

			scrMove.AddRotation(idealRotation / timeRotating);
		}
    }

	void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
	}
}
