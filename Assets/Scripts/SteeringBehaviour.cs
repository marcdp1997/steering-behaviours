using UnityEngine;

[RequireComponent(typeof(Move))]
public abstract class SteeringBehaviour : MonoBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes   
    // -----------------------------------------------------------------------------------

    private Move _move = null;
    private Vector3 _steeringForce = Vector3.zero;

    [SerializeField]
    [Range(1, 10)] private int _priority = 1;

    #endregion
    // -----------------------------------------------------------------------------------
    #region Getters and Setters
    // -----------------------------------------------------------------------------------

    public Vector3 steeringForce
    {
        get { return _steeringForce; }
        set { _steeringForce = value; }
    }

    public int priority
    {
        get { return _priority; }
        set { _priority = value; }
    }

    protected Move move
    {
        get { return _move; }
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour  
    // -----------------------------------------------------------------------------------

    private void Awake()
    {
        _move = GetComponent<Move>();
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Public Manipulators
    // -----------------------------------------------------------------------------------

    // To indicate that this class is intended only to be a base class of other classes,
    // not instantiated on its own, we use abstract.
    public abstract void PerformSteeringBehavior();

    #endregion
    // -----------------------------------------------------------------------------------
}