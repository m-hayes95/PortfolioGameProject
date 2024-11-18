using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Environment
{
    public class CraneGame : MonoBehaviour
    {
        [SerializeField, Tooltip("Select the claw game object that will be moving and grabbing items")] 
        private GameObject claw;
        [SerializeField] private float moveSpeed;
        [SerializeField, Tooltip("How far the claw will extend downwards when grabbing")] 
        private float clawExtensionLength;
        private Rigidbody rb;
        [SerializeField] private bool useCraneControls;
        private bool upUsed, downUsed, rightUsed, activated;
        [SerializeField]private bool moveTaskCompleted = false;
        private bool isCraneMoving = false;
        private Vector3 startPosition;
        private Vector3 lowerPosition;
        private Queue<Action> craneActions;

        private void Start()
        {
            rb = claw.GetComponent<Rigidbody>();
            upUsed = false;
            downUsed = false;
            rightUsed = false;
            activated = false;
            startPosition = claw.transform.position;
            craneActions = new Queue<Action>();
            PopulateActionQueue();
            StartCoroutine(HandleCraneNonPlayerActions());
        }

        private void PopulateActionQueue()
        {
            // Used for all actions after the player had finished interacting with the claw machine
            craneActions.Enqueue(LowerClaw);
            craneActions.Enqueue(GrabWithClaw);
            craneActions.Enqueue(RaiseClaw);
            craneActions.Enqueue(ReturnClawHome);
            craneActions.Enqueue(DropItem);
        }
        private IEnumerator HandleCraneNonPlayerActions()
        {
            float timeout = 10f;
            float startTime = Time.time;
            
            for (int i = 0; craneActions.Count > 0; i++)
            {
                Action action = craneActions.Dequeue();
                action.Invoke();
                bool timedOut = false;
                // Break if we get stuck in an action for too long
                while (!moveTaskCompleted && !timedOut)
                {
                    if (Time.time - startTime > timeout)
                    {
                        timedOut = true;
                    }
                    yield return null;
                }

                if (timedOut)
                {
                    Debug.LogWarning("Timeout reached. Exiting the loop.");
                    yield break;
                }
                // Reset time for each action
                startTime = Time.time;
            }
        }
        private void Update()
        {
            if (!useCraneControls)
                return;
            if (Input.GetKey(KeyCode.RightArrow) && !rightUsed)
            {
                MoveClaw(Vector3.right);
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    rightUsed = true;
                }
            }
            if (Input.GetKey(KeyCode.UpArrow) && !upUsed)
            {
                MoveClaw(Vector3.forward);
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    upUsed = true;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow) && !downUsed)
            {
                MoveClaw(Vector3.back);
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    downUsed = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && !activated)
            {
                GrabWithClaw();
                activated = true;
            }
        }

        private void MoveClaw(Vector3 direction)
        {
            rb.linearVelocity = direction * moveSpeed;
        }

        private void LowerClaw()
        {
            moveTaskCompleted = false;
            Debug.Log($"Lower claw");
            // Set the position we will return to
            startPosition = transform.position;
            lowerPosition = new Vector3(0f, clawExtensionLength, 0f);
            StartCoroutine(LerpClaw(transform.position, transform.position - lowerPosition, 0f, 2f));
        }
        private void GrabWithClaw()
        {
            moveTaskCompleted = false;
            Debug.Log($"Grab with claw");
            StartCoroutine(Timer());
        }

        private void RaiseClaw()
        {
            moveTaskCompleted = false;
            Debug.Log($"Raise claw");
            StartCoroutine(LerpClaw(transform.position, startPosition, 0f, 2f));
        }

        private void ReturnClawHome()
        {
            moveTaskCompleted = false;
            Debug.Log($"Return Home claw");
            StartCoroutine(Timer());
        }

        private void DropItem()
        {
            moveTaskCompleted = false;
            Debug.Log($"Drop item claw");
        }

        private IEnumerator Timer() // Used for testing -- Remove
        {
            yield return new WaitForSeconds(2f);
            moveTaskCompleted = true;
            Debug.Log($"Move task has been changed to true");
        }
        private IEnumerator LerpClaw(Vector3 startPos, Vector3 endPos, float delay, float lerpDuration)
        {
            yield return new WaitForSeconds(delay);
            isCraneMoving = true;
            for (float t = 0f; t < lerpDuration; t += Time.deltaTime)
            {
                claw.transform.position = Vector3.Lerp(
                    startPos, endPos, t / lerpDuration
                );
                yield return 0;
            }
            moveTaskCompleted = true;
            Debug.Log($"Move task has been changed to true");
            isCraneMoving = false;
        }
    }
}
