using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;

    private Rigidbody rb;
    private Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckInput()
    {
        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.z = Input.GetAxisRaw("Vertical") * speed;
    }

    private void ApplyMovement()
    {
        rb.velocity = velocity;
    }

    public Vector3 GetVelocity() { return velocity; }

}
