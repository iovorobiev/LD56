using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ui
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextColorChanger : MonoBehaviour
    {
        public Color defaultColor;
        public List<Color> colors;

        public float colorChangeSpeed = 0.2f;
        public bool animateByDefault;
        
        private TextMeshProUGUI tmp;
        private Coroutine animatingCoroutine = null;
        private int currentColor = 0;

        private void Start()
        {
            tmp = GetComponent<TextMeshProUGUI>();
            if (animateByDefault)
            {
                Animate();
            }
            else
            {
                StopAnimating();
            }
        }

        public void Animate()
        {
            if (colors.Count < 2)
            {
                return;
            }
            animatingCoroutine = StartCoroutine(ActuallyAnimate());
        }

        IEnumerator ActuallyAnimate()
        {
            float time = 0f;
            while (true)
            {
                time += Time.deltaTime * colorChangeSpeed;
                var nextColor = (currentColor + 1) % colors.Count;
                var actualColor = Color.Lerp(colors[currentColor], colors[nextColor], time);
                tmp.color = actualColor;
                if (time >= 1f)
                {
                    time = 0f;
                    currentColor = nextColor;
                }

                yield return new WaitForNextFrameUnit();
            }
        }

        public void StopAnimating()
        {
            if (animatingCoroutine != null)
            {
                StopCoroutine(animatingCoroutine);
                animatingCoroutine = null;
            }
            tmp.color = defaultColor;
        }
    }
}