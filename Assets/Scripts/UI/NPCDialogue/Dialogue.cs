using UnityEngine;

namespace UI.NPCDialogue
{
    [System.Serializable]
    public class Dialogue
    {
        public string npcName;
        [TextArea(2,10)] public string[] sentence;
    }
}

