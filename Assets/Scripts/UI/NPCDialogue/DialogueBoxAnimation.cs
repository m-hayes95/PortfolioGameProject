using UnityEngine;
using DG.Tweening;

namespace UI.NPCDialogue
{
    public class DialogueBoxAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private float targetLocationY;
        [SerializeField] private float tweenDuration;

        public void ShowDialogueBox()
        {
            dialogueBox.transform.DOMoveY(targetLocationY, tweenDuration).SetEase(Ease.OutBounce);
        }

        public void HideDialogueBox()
        {
            dialogueBox.transform.DOMoveY(-targetLocationY, tweenDuration);
        }
    }
}
