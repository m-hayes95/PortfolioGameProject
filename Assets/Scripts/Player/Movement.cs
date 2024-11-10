using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField, Range(1f, 20f)] float speed = 10f;
        private Rigidbody _rb;
        private Vector2 moveDirection;
        private bool allowPlayerInput = true;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
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
            _rb.linearVelocity = new Vector3(moveDirection.x * speed, 0f, moveDirection.y * speed);
        }
    }
}