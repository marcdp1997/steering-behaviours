using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class my_ray
{
    public float length = 2.0f; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
    public float direction_offset = 0.0f;
}

public class SteeringSeparation : MonoBehaviour
{
    private Move move;
    private SteeringArrive arrive;

    public my_ray[] rays;
    public float max_avoid_force;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        arrive = GetComponent<SteeringArrive>();
    }

    // Update is called once per frame
    void Update()
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
                Vector3 steering_force = hit.normal.normalized * max_avoid_force;
                steering_force.y = 0.0f;
                move.AddSteeringForce(steering_force);
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