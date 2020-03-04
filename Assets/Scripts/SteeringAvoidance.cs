using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomRay
{
    public float length; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
    public float directionOffset; // Final position offset of the ray
}

public class SteeringAvoidance : MonoBehaviour
{
    private Move scrMove;

    private Quaternion q;
    private float angle;
    private Vector3 direction;
    private Vector3 steeringForce;

    public CustomRay[] rays;
    public float maxAvoidForce = 0.1f;

    void Awake()
    {
        scrMove = GetComponent<Move>();
    }

    void FixedUpdate()
    {
        if (scrMove.GetVelocity() != Vector3.zero)
        {
            RaycastHit hit;
            angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
            q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            foreach (CustomRay ray in rays)
            {
                direction = Vector3.forward;
                direction.x += ray.directionOffset;

                if (Physics.Raycast(transform.position, q * direction.normalized, out hit, ray.length)
                    && hit.collider.CompareTag("Obstacle"))
                {
                    steeringForce = hit.normal * maxAvoidForce;
                    scrMove.AddVelocity(steeringForce);
                    break;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        RaycastHit hit;

        foreach (CustomRay ray in rays)
        {
            Vector3 direction = Vector3.forward;
            direction.x += ray.directionOffset;

            if (Physics.Raycast(transform.position, q * direction.normalized, out hit, ray.length)
                && hit.collider.CompareTag("Obstacle"))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(hit.point, hit.normal * 100);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (q * direction.normalized) * ray.length);
        }
    }
}