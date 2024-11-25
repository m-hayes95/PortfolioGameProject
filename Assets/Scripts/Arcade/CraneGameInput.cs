using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Arcade
{
    public class CraneGameInput : MonoBehaviour
    {
        [SerializeField] private UnityEvent switchControls;
        [SerializeField] private CraneGameMoveClaw claw;
        [SerializeField] private bool canMoveRight, canMoveUp, canMoveDown; // Serialized for debugging
        private bool isMovingRight, isMovingUp, isMovingDown;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                switchControls?.Invoke();
                Debug.Log("Switched controls");
            }
            // For some reason the claw still moves when the action map is set to Player
            HandleMovements();
        }

        public void ShowCollectedObject()
        {
            Debug.Log("Player got a ds game");
        }
        public void GrabSequence(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                claw.StartClawGrabSequence();
                Debug.Log($"Started the grab sequence {context.phase}");
            }
                
        }
        public void MoveRight(InputAction.CallbackContext context)
        {
            if (!canMoveRight)
                return;

            if (context.performed)
            {
                isMovingRight = true;
                Debug.Log($"Moving right {context.phase}");
            }
            if (context.canceled)
            {
                Debug.Log($"Stop moving right {context.phase}");
                isMovingRight = false;
                canMoveRight = false;
            }
        }
        public void MoveUp(InputAction.CallbackContext context)
        {
            if (!canMoveUp)
                return;

            if (context.performed)
            {
                isMovingUp = true;
                Debug.Log($"Moving up {context.phase}");
            }
            if (context.canceled)
            {
                Debug.Log($"Stop moving up {context.phase}");
                isMovingUp = false;
                canMoveUp = false;
            }
        }
        public void MoveDown(InputAction.CallbackContext context)
        {
            if (!canMoveDown)
                return;

            if (context.performed)
            {
                isMovingDown = true;
                Debug.Log($"Moving down {context.phase}");
            }
            if (context.canceled)
            {
                Debug.Log($"Stop moving down {context.phase}");
                isMovingDown = false;
                canMoveDown = false;
            }
        }

        private void HandleMovements()
        {
            // Use event to set is moving bool
            if (isMovingRight)
            {
                claw.MoveClaw(Vector3.right);
            }
            if (isMovingUp)
            {
                claw.MoveClaw(Vector3.forward);
            }
            if (isMovingDown)
            {
                claw.MoveClaw(Vector3.back);
            }
        }
        public void ResetMovements()
        {
            canMoveRight = true;
            canMoveDown = true;
            canMoveUp = true;
        }
    }
}
