using System;
using DG.Tweening;
using Tags;
using UnityEngine;

namespace Arcade
{
    public class CraneGameMoveClaw : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 3f), Tooltip("How fast the claw will move for the player controls")] 
        private float moveSpeed;
        [SerializeField, Range(0.1f, 3f), Tooltip("How long it will take to move from one position to the next in the sequence of events")] 
        private float tweenDuration, openHandsDuration;
        [SerializeField, Range(0f, 2f), Tooltip("How long the claw will wait in the grab position (seconds)")] 
        private float grabWaitTime;
        [SerializeField, Range(0f, 2f), Tooltip("How long the claw will wait to drop an item (seconds)")] 
        private float dropWaitTime;
        [SerializeField, Range(0f, 5f), Tooltip("How far the claw can move in each axis")] 
        private float xAmount, yAmount, zAmount;
        [SerializeField, Range(0f, 360f), Tooltip("How much the claw hands will rotate to open and close")] 
        private float xRotateAmount;
        [SerializeField, Tooltip("Add each of the claw hands here")] private GameObject[] clawHands;
        [SerializeField] private float rayLength;
        [SerializeField] private LayerMask gotchaLayerMask;

        private Vector3 homePosition;
        private bool useCraneControls;
        private bool isClawMoving, isCarryingObject;

        private void Start()
        {
            // Return to this position when returning home
            homePosition = transform.position;
        }

        private void FixedUpdate()
        {
            TryCarryObject();
        }

        public void MoveClaw(Vector3 moveDirection)
        {
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            Debug.Log($"Moved direction: {moveDirection}");
        }
        public void StartClawGrabSequence()
        {
            if (isClawMoving)
                return;
            Debug.Log("Claw is doing grab sequence");
            isClawMoving = true;
            // Move the claw to the gotcha ball in sequence (X -> Z - -> Open Claw -> Y -> Wait -> Complete)
            var moveClaw = DOTween.Sequence();
            OpenCloseClawTweenSequence(moveClaw, 1f, xRotateAmount);
            moveClaw.Append(transform.DOLocalMoveY(transform.position.y - yAmount, tweenDuration));
            moveClaw.PrependInterval(grabWaitTime);
            moveClaw.OnComplete(ReturnClawHome);
        }

        private void ReturnClawHome()
        {
            // Raise the claw and return home in sequence (Close Claw -> Y -> Z -> X -> Wait -> OnComplete)
            var moveClaw = DOTween.Sequence();
            OpenCloseClawTweenSequence(moveClaw, 1f, -xRotateAmount);
            moveClaw.Append(transform.DOLocalMoveY(transform.position.y + yAmount, tweenDuration));
            moveClaw.Append(transform.DOLocalMoveZ(homePosition.z , tweenDuration));
            moveClaw.Append(transform.DOLocalMoveX(homePosition.x, tweenDuration));
            moveClaw.OnComplete(OnReturnHome);
        }

        private void OnReturnHome()
        {
            // Open claw to Drop item and close after waiting
            var moveClaw = DOTween.Sequence();
            OpenCloseClawTweenSequence(moveClaw, 1, xRotateAmount);
            moveClaw.PrependInterval(dropWaitTime);
            moveClaw.OnComplete(OnCraneGameFinished);
        }
        private void OnCraneGameFinished()
        {
            UnParentObjectAndApplyForce();
            var moveClaw = DOTween.Sequence();
            OpenCloseClawTweenSequence(moveClaw, 1, -xRotateAmount);
            Debug.Log("Finished Crane Game");
            isClawMoving = false;
        }
        private void OpenCloseClawTweenSequence(Sequence sequence, float sequencePosition, float rotateAmount)
        {
            foreach (GameObject claw in clawHands)
            {
                Vector3 currentRotation = claw.transform.rotation.eulerAngles;
                Vector3 targetRotation = new Vector3(currentRotation.x + rotateAmount, currentRotation.y, currentRotation.z);
                sequence.Insert(sequencePosition, claw.transform.DORotate(targetRotation, openHandsDuration, RotateMode.Fast));
            }
        }

        private void TryCarryObject()
        {
            Vector3 origin = transform.position;
            if (Physics.Raycast(origin, Vector3.down, out var hit, rayLength, gotchaLayerMask) && !isCarryingObject)
            {
                Debug.Log("Found gotcha ball");
                isCarryingObject = true;
                ChildObject(hit);
            }
            Debug.DrawRay(origin, Vector3.down * rayLength, Color.cyan);
        }

        private void ChildObject(RaycastHit hit)
        {
            hit.transform.SetParent(transform);
        }

        private void UnParentObjectAndApplyForce()
        {
            if (GetComponentInChildren<GotchaTag>())
            {
                GameObject child = GetComponentInChildren<GotchaTag>().gameObject;
                child.transform.SetParent(null);
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.down * 1f, ForceMode.Impulse);
                }
            }
            else
                Debug.Log("There is no child Gotcha ball attached to the claw");
        }
    }
}

