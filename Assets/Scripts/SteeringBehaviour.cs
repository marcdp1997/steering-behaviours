using UnityEngine;

[RequireComponent(typeof(AIController))]
public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField] protected bool drawGizmos;
    [SerializeField] protected int priority;

    protected AIController aiController;
    protected Vector3 steeringForce;
    protected Vector3 targetPosition;

    private void Awake()
    {
        aiController = GetComponent<AIController>();
    }

    protected virtual void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + steeringForce);
    }

    public virtual void UpdateSteeringBehavior() 
    {
        steeringForce = Vector3.zero;
        targetPosition = aiController.GetTarget().transform.position;
    }

    public Vector3 GetSteeringForce() { return steeringForce; }

    public int GetPriority() { return priority; }
}