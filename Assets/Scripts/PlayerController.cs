﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -----------------------------------------------------------------------------------
    #region Attributes   

    [SerializeField] private float speed = 10.0f;

    private Rigidbody rb;
    private Vector3 movement;

    #endregion
    // -----------------------------------------------------------------------------------
    #region MonoBehaviour  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    #endregion
    // -----------------------------------------------------------------------------------
    #region Private Manipulators

    private void CheckInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.z = Input.GetAxisRaw("Vertical") * speed;
    }

    private void ApplyMovement()
    {
        rb.velocity = movement;
    }

    #endregion
    // -----------------------------------------------------------------------------------
}
