using UnityEngine;
namespace UI.NPCDialogue
{
    public class DialogueElement : MonoBehaviour, IInteractable
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private DialogueManager manager;
        private bool inDialogue;
        
        public void Interact()
        {
            if (!inDialogue)
            {
                TriggerDialogue();
                inDialogue = true;
            }
            else
            {
                TriggerNextDialogue();
            }
            
        }
        private void TriggerDialogue()
        {
            manager.StartDialogue(dialogue);
        }

        private void TriggerNextDialogue()
        {
            manager.DisplayNextSentence();
        }

        public void ResetDialogue()
        {
            inDialogue = false;
        }
        
        
    }
}

