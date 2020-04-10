using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SteeringArrive : SteeringBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes 
    // -----------------------------------------------------------------------------------

    [Range(0, 1)] private float slowFactor = 1.0f;
    private float distance = 0.0f;
    private bool _arrived = false;
    private bool _arriving = false;
    private float stopOffset = 0.5f;

    public Text debugText;
    public float stopRadius = 4.0f;
    public float slowRadius = 6.0f;

    #endregion
    // -----------------------------------------------------------------------------------
    #region Getters and setters
    // -----------------------------------------------------------------------------------

    public bool arrived
    {
        get { return _arrived; }
        private set { _arrived = value; }
    }

    public bool arriving
    {
        get { return _arriving; }
        private set { _arriving = value; }
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour
    // -----------------------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        if (move == null)
        {
            return;
        }

        Handles.color = Color.white;
        Handles.DrawWireDisc(move.target.transform.position, transform.up, slowRadius);
        Handles.DrawWireDisc(move.target.transform.position, transform.up, stopRadius);
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Steering 
    // -----------------------------------------------------------------------------------

    public override void PerformSteeringBehavior()
    {
        ResetPrevInfo();

        distance = (move.target.transform.position - transform.position).magnitude;
      
        if (distance <= slowRadius)
        {
            slowFactor = (distance - stopRadius) / (slowRadius - stopRadius);
            arriving = true;
        }

        if (distance <= stopRadius + stopOffset)
        {
            slowFactor = 0;
            arrived = true;
        }

        Vector3 desiredVelocity = (move.target.transform.position - transform.position).normalized * move.maxSpeed * slowFactor;
        steeringForce = desiredVelocity - move.velocity;

        DrawDebugText();
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Private manipulators
    // -----------------------------------------------------------------------------------

    private void ResetPrevInfo()
    {
        arrived = false;
        arriving = false;
        slowFactor = 1;
    }

    private void DrawDebugText()
    {
        debugText.text = "Slow Factor: " + slowFactor.ToString("F2") + "\n" +
                         "Distance: " + distance.ToString("F2") + "\n" +
                         "Arrived: " + arrived.ToString() + "\n" +
                         "Arriving: " + arriving.ToString() + "\n";
    }

    #endregion
    // -----------------------------------------------------------------------------------
}