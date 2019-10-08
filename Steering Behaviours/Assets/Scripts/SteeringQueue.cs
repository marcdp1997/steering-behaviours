using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class queue_ray
{
    public float length = 2.0f; // The greater this value is, the earlier the character will start acting to dodge an obstacle.
}

public class SteeringQueue : MonoBehaviour
{
    private Move move;
    private SteeringArrive arrive;
    private SteeringAvoidance avoidance;

    public queue_ray[] queue_rays;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        arrive = GetComponent<SteeringArrive>();
    }

    // Update is called once per frame
    void Update()
    {
        //move.doing_queue = false;

        if (move.velocity != Vector3.zero)
        {
            RaycastHit hit;
            float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            foreach (queue_ray ray in queue_rays)
            {
                Vector3 direction = Vector3.forward;

                if (Physics.Raycast(transform.position, q * direction.normalized, out hit, ray.length))
                {
                    //move.doing_queue = true;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        foreach (queue_ray ray in queue_rays)
        {
            Vector3 direction = Vector3.forward;
            Gizmos.DrawRay(transform.position, (q * direction.normalized) * ray.length);
        }
    }
}
