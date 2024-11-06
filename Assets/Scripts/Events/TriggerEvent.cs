using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEventEnter, triggerEventExit, interactEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTag>())
        {
            triggerEventEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerTag>())
        {
            triggerEventExit.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactEvent.Invoke();
        }
    }
}
