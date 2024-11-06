using UnityEngine;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [SerializeField, Range(0f,20f)] private float interactableDistance;
        [SerializeField] private LayerMask interactable;
        
        public void InteractWithObject()
        {
            GameObject interactableObject = FindClosestGameObject();
            if (interactableObject == null) return;
            Debug.Log($"Found Interactable object: {FindClosestGameObject().gameObject.name}");

            var canInteract = interactableObject.GetComponent<IInteractable>();
            canInteract?.Interact();
        }
        private GameObject FindClosestGameObject()
        {
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
