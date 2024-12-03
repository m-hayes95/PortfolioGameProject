using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputSwitcher : MonoBehaviour
    {
        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            // Disable all action maps at the start to ensure no input is being processed
            DisableAllActionMaps();

            // Then enable the Player action map as the default
            SwitchToPlayerActionMap();
        }

        private void Update()
        {
            Debug.Log($"Current Player input = {playerInput.currentActionMap}");
            if (Input.GetKeyDown(KeyCode.P))
            {
                SwitchToPlayerActionMap();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SwitchToCraneActionMap();
            }
        }

        public void SwitchToPlayerActionMap()
        {
            // Disable CraneGame action map explicitly if it's active
            DisableActionMap("CraneGame");

            // Enable Player action map
            playerInput.SwitchCurrentActionMap("Player");
            if (playerInput.currentActionMap == null)
                Debug.LogError("Player input action map not found");
        }

        public void SwitchToUIActionMap()
        {
            // Disable CraneGame action map explicitly if it's active
            DisableActionMap("CraneGame");

            // Enable UI action map
            playerInput.SwitchCurrentActionMap("UI");
            if (playerInput.currentActionMap == null)
                Debug.LogError("UI input action map not found");
        }

        public void SwitchToCraneActionMap()
        {
            // Disable Player action map explicitly if it's active
            DisableActionMap("Player");

            // Enable CraneGame action map
            playerInput.SwitchCurrentActionMap("CraneGame");
            if (playerInput.currentActionMap == null)
                Debug.LogError("CraneGame input action map not found");
        }

        private void DisableActionMap(string actionMapName)
        {
            // Find the action map by name
            InputActionMap actionMap = playerInput.actions.FindActionMap(actionMapName);
            if (actionMap != null)
            {
                // Disable the specific action map
                actionMap.Disable();
                Debug.Log($"Disabled action map: {actionMapName}");
            }
            else
            {
                Debug.LogWarning($"Can't find action map with name: {actionMapName}");
            }
        }

        private void DisableAllActionMaps()
        {
            // Go through all action maps and disable them
            foreach (var actionMap in playerInput.actions)
            {
                actionMap.Disable();
                Debug.Log($"Disabled all action maps.");
            }
        }

    }
}

