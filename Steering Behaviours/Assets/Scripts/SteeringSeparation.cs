using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour
{
    public float max_repulsion;

    private Move move;
    private SphereCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        int layer_id = 8;
        int layer_mask = 1 << layer_id;
        Collider[] hitColliders = Physics.OverlapSphere(collider.transform.position, collider.radius, layer_mask);

        Vector3 steering_force = Vector3.zero;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Vector3 force = hitColliders[i].transform.position - collider.transform.position;
            force = force * -max_repulsion;
            steering_force += force;
        }

        move.AddVelocity(steering_force);
    }
}
