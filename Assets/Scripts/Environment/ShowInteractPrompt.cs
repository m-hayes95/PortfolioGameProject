using UnityEngine;

public class ShowInteractPrompt : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    public void DisplayInteractPrompt()
    {
        promptUI.SetActive(true);
    }

    public void HideInteractPrompt()
    {
        promptUI.SetActive(false);
    }
}
