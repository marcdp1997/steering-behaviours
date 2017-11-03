using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class my_ray
{
    public float length = 2.0f; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
    public Vector3 direction = Vector3.forward;
}

public class SteeringSeparation : MonoBehaviour
{
    private Move move;

    public float max_avoid_force = 10.0f;
    public my_ray[] rays;
    Vector3 avoidance_force = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        foreach (my_ray ray in rays)
        {
            ray.direction = transform.forward;
            avoidance_force = Vector3.zero;

            if (Physics.Raycast(transform.position, ray.direction.normalized, out hit, ray.length))
            {
                avoidance_force = hit.normal.normalized * max_avoid_force;
                avoidance_force.y = 0.0f;
                move.AddSteeringForce(avoidance_force);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (my_ray ray in rays)
            Gizmos.DrawRay(transform.position, ray.direction.normalized * ray.length);
    }
}