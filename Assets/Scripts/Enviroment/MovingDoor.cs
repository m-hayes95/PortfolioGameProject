using UnityEngine;
using System.Collections;

public class MovingDoor : MonoBehaviour
{
    [SerializeField] private Transform goalTransform;
    [SerializeField] private float openDuration, closeDuration;
    [SerializeField] private GameObject movingObject;
    [SerializeField, Range(0f, 10f)] private float closeDelay = 0f;
    [SerializeField, Range(0f, 20f)] private float collisionRadius = 10f;
    [SerializeField] private Vector3 collisionOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField]private bool isDoorOpen = false;
    [SerializeField]private bool isDoorMoving = false;
    [SerializeField]private bool isPlayerInRange = false;
    private Vector3 startPosition;
    
    private void Start()
    {
        startPosition = movingObject.transform.position;
    }

    private void FixedUpdate()
    {
        // Force the door closed if it is currently open and the trigger event close door did not work
        // This happens when the player leaves the trigger area and the door is currently opening
        if (!isDoorOpen || isDoorMoving)
            return;
        if (!CheckForPlayerCollisions())
        {
            Debug.Log("Door Forced Close");
            CloseDoor();
        }
    }

    private bool CheckForPlayerCollisions()
    {
        bool playerHit = false;
        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position + collisionOffset, collisionRadius, playerLayerMask);
        if (hitColliders.Length == 0)
        {
            return false;
        }

        if (hitColliders[0].GetComponent<PlayerTag>())
        {
            Debug.Log($"Actor: {hitColliders[0].name} is in the door area");
            playerHit = true;
        }

        return playerHit;
    } 
    
    public void OpenDoor()
    {
        if (!isDoorOpen && !isDoorMoving)
        {
            StartCoroutine(LerpDoor(startPosition, goalTransform.position, 0f, openDuration));
            isDoorOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isDoorOpen && !isDoorMoving)
        {
            StartCoroutine(LerpDoor(movingObject.transform.position, startPosition, closeDelay, closeDuration));
            isDoorOpen = false;
        }
    }

    private IEnumerator LerpDoor(Vector3 startPos, Vector3 endPos, float delay, float lerpDuration)
    {
        yield return new WaitForSeconds(delay);
        isDoorMoving = true;
        for (float t = 0f; t < lerpDuration; t += Time.deltaTime)
        {
            movingObject.transform.position = Vector3.Lerp(
                startPos, endPos, t / lerpDuration
               );
            yield return 0;
        }

        movingObject.transform.position = isDoorOpen ? goalTransform.position : startPosition;
        isDoorMoving = false;
    }
}
