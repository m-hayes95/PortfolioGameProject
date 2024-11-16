using UnityEngine;

namespace UI
{
    public class OpenUrl : MonoBehaviour
    {
        public void OpenWebpage(string urlName)
        {
            Application.OpenURL(urlName);
        }
    }
}
