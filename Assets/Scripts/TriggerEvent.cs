using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEvent1, triggerEvent2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTag>())
        {
            triggerEvent1.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerTag>())
        {
            triggerEvent2.Invoke();
        }
    }
}
