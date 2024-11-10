using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [SerializeField, Range(0f,20f)] private float interactableDistance;
        [SerializeField] private LayerMask interactable;
        
        public void InteractWithObject(InputAction.CallbackContext context)
        {
            // without using context confirmation, the event will be called for each phase
            if (!context.started)
                return;
            Debug.Log($"Interact: {context.phase}");
            GameObject interactableObject = FindClosestGameObject();
            if (interactableObject == null) return;
            Debug.Log($"Found Interactable object: {FindClosestGameObject().gameObject.name}");

            var canInteract = interactableObject.GetComponent<IInteractable>();
            canInteract?.Interact();
        }
        private GameObject FindClosestGameObject()
        {
            // Need to change to a box cast
            GameObject objectFound = null;
            if (Physics.Raycast(
                    transform.position, Vector3.forward, out var hit, interactableDistance, interactable
                    ))
            {
                objectFound = hit.transform.gameObject;
                Debug.DrawRay(transform.position, Vector3.forward * interactableDistance, Color.cyan);
            }
            else
                Debug.DrawRay(transform.position, Vector3.forward * interactableDistance, Color.red);
            
            return objectFound;
        }
        
    }
}
