using UnityEngine;
using UnityEngine.Events;

namespace Arcade
{
    public class CraneGame : MonoBehaviour
    {
        [SerializeField] private UnityEvent playCraneGame;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playCraneGame?.Invoke();
            }
        }

        public void ShowCollectedObject()
        {
            Debug.Log("Player got a ds game");
        }
    }
}
