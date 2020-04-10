using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWander : MonoBehaviour
{
    private Move scrMove;
    private SteeringArrive scrArrive;

    private Vector3 vCenter;
    private float angle;
    private float x, y, z;
    private float timeToChange;

    public float searchRadius = 10.0f;
    public float minTime = 5.0f;
    public float maxTime = 10.0f;

    // Start is called before the first frame update
    void Awake()
    {
        scrMove = GetComponent<Move>();
        scrArrive = GetComponent<SteeringArrive>();

        timeToChange = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeToChange -= Time.deltaTime;

        if (timeToChange <= 0 || scrArrive.arriving)
        {
            vCenter = transform.position;
            angle = Random.Range(0, 360);

            x = Mathf.Cos(angle) * searchRadius + vCenter.x;
            y = vCenter.y;
            z = Mathf.Sin(angle) * searchRadius + vCenter.z;

            scrMove.target.transform.position = new Vector3(x, y, z);
            timeToChange = Random.Range(minTime, maxTime);
        }     
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
