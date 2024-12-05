using UnityEngine;
using System.Collections;
using TMPro;

namespace UI
{
    public class FlashClickHere : MonoBehaviour
    {
        [SerializeField] private float textFadeTimer;
        [SerializeField] private TMP_Text text;
        private Color transparentColor;
        private Color originalColor;
        private bool doOnce;

        private void Start()
        {
            SetColors();
        }
        private void Update()
        {
            if (gameObject.activeSelf && !doOnce)
            {
                doOnce = true;
                StartCoroutine(FlashTextOff());
            }
        }

        private void SetColors()
        {
            transparentColor = new Color(0f, 0f, 0f, 0f);
            originalColor = text.color;
        }
        
        private IEnumerator FlashTextOff()
        {
            // flash the text on and off after a set amount of time
            yield return new WaitForSeconds(textFadeTimer);
            text.color = transparentColor;
            StartCoroutine(FlashTextOn());
        }
        private IEnumerator FlashTextOn()
        {
            // flash the text on and off after a set amount of time
            yield return new WaitForSeconds(textFadeTimer * 0.5f);
            text.color = originalColor;
            doOnce = false;
        }
    }
}

