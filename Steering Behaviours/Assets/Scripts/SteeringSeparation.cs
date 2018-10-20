using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class my_ray
{
    public float length; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
    public float direction_offset; // Final position offset of the ray
}

public class SteeringSeparation : MonoBehaviour
{
    private Move move;
    private SteeringArrive arrive;

    public my_ray[] rays;
    public float max_avoid_force = 0.1f;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move.velocity != Vector3.zero)
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

                    //if (ray.direction_offset < 0) steering_force = transform.right.normalized * max_avoid_force;
                    //else steering_force = -transform.right.normalized * max_avoid_force;

                    move.AddVelocity(steering_force);
                    break;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        foreach (my_ray ray in rays)
        {
            Vector3 direction = Vector3.forward;
            direction.x += ray.direction_offset;
            Gizmos.DrawRay(transform.position, (q * direction.normalized) * ray.length);
        }    
    }
}