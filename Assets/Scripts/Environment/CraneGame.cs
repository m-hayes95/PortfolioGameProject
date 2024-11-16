using System.Collections;
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
        private bool isCraneMoving = false;
        private Vector3 startPosition;

        private void Start()
        {
            rb = claw.GetComponent<Rigidbody>();
            upUsed = false;
            downUsed = false;
            rightUsed = false;
            activated = false;
            startPosition = claw.transform.position;
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
        private void GrabWithClaw()
        {
            // Create list of commands using queue for first in first out (Command Pattern?)
            // Lower claw
            // Grab
            // Return Claw Home
            // Drop Item
            // Reset command queue
        }

        private void ReturnClawHome()
        {
            
        }

        private void DropItem()
        {
            
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
            isCraneMoving = false;
        }
        
        
    }
}
