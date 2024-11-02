using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField, Range(1f, 1000f)] float speed = 10f;
    private PlayerInput playerInput;
    private Rigidbody _rb;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        Vector2 inputVectorNormalized = playerInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVectorNormalized.x, 0f, inputVectorNormalized.y);
        _rb.MovePosition(transform.position + moveDirection * (speed * Time.deltaTime));
    }
}
