using UnityEngine;

namespace Environment
{
    public class ArcadeMachine : MonoBehaviour, IInteractable
    {
        private void PlayMachine()
        {
            Debug.Log($"Play arcade Machine {gameObject.name}");
        }
        public void Interact()
        {
            PlayMachine();
        }
    }
}



