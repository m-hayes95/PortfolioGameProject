using UnityEngine;
using System.Collections;
using UnityEngine.Events;
namespace Environment
{
    public class ArcadeMachine : MonoBehaviour, IInteractable
    {
        [SerializeField] private SwitchCamera switchCamera;
        [SerializeField, Range(0,2)] private int machineNumber;
        [SerializeField, Range(0,10f)] private float switchDelay;
        [SerializeField] private UnityEvent enterPlayMode, exitPlayMode;
        private bool canPlay = true;
        private void PlayMachine()
        {
            Debug.Log($"Play arcade Machine {gameObject.name}");
            switchCamera.SwitchToArcadeCamera(machineNumber);
            enterPlayMode?.Invoke();
        }
        private void ExitMachine()
        {
            Debug.Log($"Stop Playing {gameObject.name} ");
            switchCamera.ExitArcadeCamera();
            exitPlayMode?.Invoke();
        }

        private IEnumerator KeepInPlayMode()
        {
            yield return new WaitForSeconds(switchDelay);
            canPlay = false;
        }
        private IEnumerator KeepPlayModeDisabled()
        {
            yield return new WaitForSeconds(switchDelay);
            canPlay = true;
        }
        public void Interact()
        {
            // Without delays to switching cameras, the camera switching can get stuck
            switch (canPlay)
            {
                case true:
                    PlayMachine();
                    StartCoroutine(KeepInPlayMode());
                    break;
                
                case false:
                    ExitMachine();
                    StartCoroutine(KeepPlayModeDisabled());
                    break;
            }
        }
    }
}



