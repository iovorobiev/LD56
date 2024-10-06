using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ui
{
    public class TextTyping : MonoBehaviour
    {
        public float typeDelay;
        private TextMeshProUGUI tmp;
        private AudioSource _typeSound;
        private Coroutine lastCoroutine;

        private void Start()
        {
            tmp = GetComponent<TextMeshProUGUI>();
            _typeSound = GetComponent<AudioSource>();
            var text = tmp.text;
            animateText(text);
        }

        public void animateText(String text)
        {
            if (lastCoroutine != null)
            {
                StopCoroutine(lastCoroutine);
            }
            lastCoroutine = StartCoroutine(AnimateText(text));
        }

        private IEnumerator AnimateText(String text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                tmp.text = text.Substring(0, i + 1);
                if (_typeSound != null)
                {
                    _typeSound.Play();
                }
                yield return new WaitForSeconds(typeDelay);
            }
        }
    }
}