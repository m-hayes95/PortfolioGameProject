using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField, Range(1f, 20f)] float moveSpeed = 10f;
        [SerializeField, Range(1f, 20f)] float rotateSpeed = 10f;
        private Rigidbody rb;
        private Vector2 moveDirection;
        private bool allowPlayerInput = true;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }
        
        #region Called through Events
        public void GetInputVectorNormalized(InputAction.CallbackContext context)
        {
            // normalized in  input actions file
            moveDirection = context.ReadValue<Vector2>();
        }
        public void EnablePlayerMovement()
        {
            allowPlayerInput = true;
        }
        public void DisablePlayerMovement()
        {
            allowPlayerInput = false;
        }
        #endregion
        
        private void HandleMovement()
        {
            if (!allowPlayerInput)
                return;
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, 0f, moveDirection.y * moveSpeed);
            LookForward();
        }

        private void LookForward()
        {
            Vector3 forwardMovement = new Vector3(moveDirection.x, 0f, moveDirection.y);
            transform.forward = Vector3.Slerp(transform.forward, forwardMovement, (Time.deltaTime * rotateSpeed));
        }
    }
}