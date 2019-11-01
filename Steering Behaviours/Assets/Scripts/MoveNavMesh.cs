using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveNavMesh : MonoBehaviour
{
    private Move move;
    private SteeringArrive arrive;
    private NavMeshAgent nav_agent;

    private Vector3[] path;
    private Vector3 final_target;

    [Header("------ Read Only -------")]
    public int progress = 0;

    [Header("------ Set Values ------")]
    public float path_point_distance = 1.5f;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        arrive = GetComponent<SteeringArrive>();
        nav_agent = GetComponent<NavMeshAgent>();

        final_target = move.target.transform.position;
        SetDestination(final_target);
    }

    // Update is called once per frame
    void Update()
    {
        nav_agent.isStopped = true;
        nav_agent.updateRotation = false;

        if (nav_agent.hasPath)
        {
            if (!nav_agent.pathEndPosition.Equals(path[path.Length - 1]))
                path = nav_agent.path.corners;

            if (progress < path.Length)
            {
                if (Vector3.Distance(transform.position, path[progress]) < path_point_distance)
                {
                    progress++;
                }
                if (progress < path.Length)
                {
                    move.target.transform.position = path[progress];
                }
            }
        }
    }

    public bool IsPathFinishing()
    {
        return (progress >= path.Length - 1);
    }

    public void SetDestination(Vector3 dest)
    {
        progress = 0;

        final_target = dest;
        NavMeshPath nav_path = new NavMeshPath();
        nav_agent.SetDestination(final_target);
        nav_agent.CalculatePath(final_target, nav_path);
        path = nav_agent.path.corners;
    }

    public void OnDrawGizmos()
    {
        for (int i = 0; i < nav_agent.path.corners.Length - 1; i++)
            Debug.DrawLine(nav_agent.path.corners[i], nav_agent.path.corners[i + 1], Color.red);
    }
}
