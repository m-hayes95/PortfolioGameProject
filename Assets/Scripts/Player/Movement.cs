using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField, Range(1f, 20f)] float moveSpeed = 10f;
        [SerializeField, Range(1f, 20f)] float rotateSpeed = 10f;
        [SerializeField, Range(1f, 5f)] float timeToCallIdleAnim = 2f;
        [SerializeField, Range(1f, 5f)] float timerReset = 5f;
        [SerializeField] private string[] idleVariationTriggers;
        private Rigidbody rb;
        private Animator animator;
        private Vector2 moveDirection;
        private float currentTime;
        private int lastIdleAnimPlayed;
        private bool allowPlayerInput = true;
        private bool doOnce = true;
        private const string IS_MOVING = "IsMoving";
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            currentTime = timeToCallIdleAnim;
        }

        private void Update()
        {
            HandleAnimations();
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

        private void HandleAnimations()
        {
            // Uses Input to set animation bool for moving is true or false
            animator.SetBool(IS_MOVING, moveDirection != Vector2.zero);
            if (doOnce)
            {
                Timer();
            }
        }
        private void Timer()
        {
            if (currentTime <= 0f)
            {
                doOnce = false;
                int rand = Random.Range(0, idleVariationTriggers.Length);
                while (rand == lastIdleAnimPlayed)
                {
                    rand = Random.Range(0, idleVariationTriggers.Length);
                }
                lastIdleAnimPlayed = rand;
                string trigger = idleVariationTriggers[rand];
                animator.SetTrigger(trigger);
                Invoke(nameof(ResetTimer), timerReset);
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
        private void ResetTimer()
        {
            doOnce = true;
            currentTime = timeToCallIdleAnim;
        }
    }
}