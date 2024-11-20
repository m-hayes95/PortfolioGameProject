using Tags;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GotchaBallCollectedEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent collectedGotchaEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GotchaTag>())
            {
                collectedGotchaEvent?.Invoke();
            }
        }
    }
}

