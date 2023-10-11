using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacterInfo
{
    [SerializeField] private float speed = 10.0f;

    private Vector3 velocity;

    private void Update()
    {
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        velocity *= speed * Time.deltaTime;
        transform.position += velocity;
    }

    public Vector3 GetPosition() { return transform.position; }

    public float GetMaxSpeed() { return speed; }

    public Vector3 GetVelocity() { return velocity; }

    public Vector3 GetForward() { return transform.forward; }
}
