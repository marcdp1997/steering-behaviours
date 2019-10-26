using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class my_ray
{
    public float length; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
    public float direction_offset; // Final position offset of the ray
}

public class SteeringAvoidance : MonoBehaviour
{
    private Move move;
    private SteeringArrive arrive;
    private SteeringQueue queue;

    public my_ray[] rays;
    public float max_avoid_force = 0.1f;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        queue = GetComponent<SteeringQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.velocity != Vector3.zero && !queue.is_in_queue)
        {
            RaycastHit hit;
            float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            foreach (my_ray ray in rays)
            {
                Vector3 direction = Vector3.forward;
                direction.x += ray.direction_offset;

                if (Physics.Raycast(transform.position, q * direction.normalized, out hit, ray.length))
                {
                    Vector3 steering_force = hit.normal * max_avoid_force;
                    move.AddVelocity(steering_force);
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

        foreach (my_ray ray in rays)
        {
            Vector3 direction = Vector3.forward;
            direction.x += ray.direction_offset;

            if (Physics.Raycast(transform.position, q * direction.normalized, out hit, ray.length))
            {
                Gizmos.color = Color.white;
                Gizmos.DrawRay(hit.point, hit.normal * 100);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (q * direction.normalized) * ray.length);
        }
    }
}