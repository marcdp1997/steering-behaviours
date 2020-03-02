using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringQueue : MonoBehaviour
{
    [Header("------ Read Only -------")]
    public bool is_in_queue = false;

    [Header("------ Set Values -------")]
    public float max_queue_ahead;
    public float max_queue_radius;
    public float max_brake_force;

    private Move move; 

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        /* 
       Vector3 ahead = transform.position + (move.velocity.normalized * max_queue_ahead);

       RaycastHit hit;
       float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
       Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

       if (Physics.Raycast(transform.position, q * Vector3.forward, out hit, max_queue_ahead))
       {
           if (!hit.collider.gameObject.CompareTag("Obstacle"))
           {
               is_in_queue = true;

               if (move.velocity.magnitude > 0.2)
               {
                   Vector3 brake_force = -Vector3.forward * (max_brake_force / 100);
                   move.AddVelocity(brake_force);
               }
               else move.SetVelocity(Vector3.zero);
           }
       }
       else is_in_queue = false;
        */
    }

    /*       
    void OnDrawGizmos()
    {
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, (q * Vector3.forward) * max_queue_ahead);
    }
    */
}