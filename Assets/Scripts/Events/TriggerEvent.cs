using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent triggerEventEnter, triggerEventExit;
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
    }
}