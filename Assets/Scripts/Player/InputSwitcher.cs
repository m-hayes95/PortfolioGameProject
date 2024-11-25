using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputSwitcher : MonoBehaviour
    {
        private PlayerInput playerInput;
        // Used in events

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            // Set the default to player
            SwitchToPlayerActionMap();
        }
        private void Update()
        {
            Debug.Log($"Current Player input = {playerInput.currentActionMap}");
        }

        public void SwitchToPlayerActionMap()
        {
            playerInput.SwitchCurrentActionMap("Player");
            if (playerInput.currentActionMap == null)
                Debug.LogError($"Player input action map not found {playerInput.currentActionMap}");
        }
        public void SwitchToUIActionMap()
        {
            playerInput.SwitchCurrentActionMap("UI");
            if (playerInput.currentActionMap == null)
                Debug.LogError($"UI input action map not found {playerInput.currentActionMap}");
        }
        public void SwitchToCraneActionMap()
        {
            playerInput.SwitchCurrentActionMap("CraneGame");
            if (playerInput.currentActionMap == null)
                Debug.LogError($"Crane input action map not found {playerInput.currentActionMap}");
        }
    }
}

