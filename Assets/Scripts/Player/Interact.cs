using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [SerializeField, Range(0f,20f)] private float interactableDistance;
        [SerializeField] private LayerMask interactableLayer;
        private Collider _collider;

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }
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
            
            if (Physics.BoxCast(_collider.bounds.center, transform.localScale*0.5f, transform.forward,
                    out var hit, transform.rotation, interactableDistance, interactableLayer))
            {
                objectFound = hit.transform.gameObject;
                Debug.DrawRay(transform.position, transform.forward * interactableDistance, Color.cyan);
            }
            else
                Debug.DrawRay(transform.position, transform.forward * interactableDistance, Color.red);
            
            return objectFound;
        }
    }
}
