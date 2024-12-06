using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

namespace UI.NPCDialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private UnityEvent onStartDialogue, onDialogueExit, onLastSentence;
        private Queue<string> sentences;

        private void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            onStartDialogue?.Invoke(); // Trigger events like disable player movement
            Debug.Log($"Stared a conversation with NPC: {dialogue.npcName}");
            nameText.text = dialogue.npcName;
            sentences.Clear();
            foreach (var sentence in dialogue.sentence)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            if (sentences.Count == 1)
            {
                LastSentence();
            }
            
            string currentSentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeOutSentence(currentSentence));
        }

        private void LastSentence()
        {
            onLastSentence?.Invoke();
        }
        
        private IEnumerator TypeOutSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
        private void EndDialogue()
        {
            Debug.Log("Ended conversation with npc");
            onDialogueExit?.Invoke();
        }
    }
}