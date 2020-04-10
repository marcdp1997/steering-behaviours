using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringQueue : MonoBehaviour
{
    private Move scrMove;
    private Vector3 vBrakeForce;

    [Header("------ Read Only -------")]
    [SerializeField] private float distance;

    [Header("------ Set Values ------")]
    public float maxAhead = 4.0f;
    public float minAhead = 1.5f;
    public float maxBrakeForce = 2.0f;

    void Start()
    {
        scrMove = GetComponent<Move>();
    }

    void FixedUpdate()
    {      
        /*
       RaycastHit hit;
       float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
       Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        // Calculating the hit point between a ray and scene colliders
        // Agents start to brake between max ahead and min ahead
        // In min ahead start waiting
        if (Physics.Raycast(transform.position, q * Vector3.forward, out hit, maxAhead))
        {
             if (hit.collider.gameObject.CompareTag("Enemy") && hit.collider.gameObject != this)
             {
                 distance = (hit.point - transform.position).magnitude;
        
                 if (distance > minAhead)
                 {
                    vBrakeForce = -Vector3.forward * maxBrakeForce;
                    scrMove.AddVelocity(vBrakeForce);
                 }
                 else scrMove.SetWaiting(true);
             }
            else scrMove.SetWaiting(false);
        }
        else scrMove.SetWaiting(false);
        */
    }    
    
    void OnDrawGizmos()
    {
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, (q * Vector3.forward) * maxAhead);

        Gizmos.color = Color.grey;
        Gizmos.DrawRay(transform.position, (q * Vector3.forward) * minAhead);
    }   
}