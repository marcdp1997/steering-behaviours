using UnityEngine;

[RequireComponent(typeof(AIController))]
public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField] protected int priority;
    protected AIController aiController;
    protected Vector3 steeringForce;

    private void Awake()
    {
        aiController = GetComponent<AIController>();
    }

    public virtual void UpdateSteeringBehavior() 
    {
        steeringForce = Vector3.zero;
    }

    public Vector3 GetSteeringForce() { return steeringForce; }

    public int GetPriority() { return priority; }
}