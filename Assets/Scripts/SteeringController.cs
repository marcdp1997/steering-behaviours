using UnityEngine;

public class SteeringController : MonoBehaviour
{
    private Agent owner;

    private ArriveBehaviour arrive;
    private AvoidanceBehaviour avoidance;
    private EvadeBehaviour evade;
    private FleeBehaviour flee;
    private PursueBehaviour pursue;
    private QueueBehaviour queue;
    private SeekBehaviour seek;
    private SeparationBehaviour separation;
    private WanderBehaviour wander;

    private void Awake()
    {
        arrive = GetComponentInChildren<ArriveBehaviour>();
        avoidance = GetComponentInChildren<AvoidanceBehaviour>();
        evade = GetComponentInChildren<EvadeBehaviour>();
        flee = GetComponentInChildren<FleeBehaviour>();
        pursue = GetComponentInChildren<PursueBehaviour>();
        queue = GetComponentInChildren<QueueBehaviour>();
        seek = GetComponentInChildren<SeekBehaviour>();
        separation = GetComponentInChildren<SeparationBehaviour>();
        wander = GetComponentInChildren<WanderBehaviour>();
    }

    public void SetOwner(Agent owner) 
    { 
        this.owner = owner; 
    }

    public Vector3 Arrive(Vector3 target) { return arrive.DoArrive(owner, target); }
    public Vector3 Avoidance() { return avoidance.DoAvoidance(owner); }
    public Vector3 Evade(ICharacterInfo target) { return evade.DoEvade(owner, target); }
    public Vector3 Flee(Vector3 target) { return flee.DoFlee(owner, target); }
    public Vector3 Pursue(ICharacterInfo target) { return pursue.DoPursue(owner, target); }
    public Vector3 Queue(Vector3 steeringForceSum) { return queue.DoQueue(owner, steeringForceSum); }
    public Vector3 Seek(Vector3 target) { return seek.DoSeek(owner, target); }
    public Vector3 Separation() { return separation.DoSeparation(owner); }
    public Vector3 Wander() { return wander.DoWander(owner); }
}