using System;
using UnityEngine;

namespace Base
{
    public class PausableBehaviour : MonoBehaviour
    {
        public static bool paused;
        
        void Update()
        {
            if (paused)
            {
                return;
            }
            PausableUpdate();
        }

        public virtual void PausableUpdate()
        {
            
        }

        private void FixedUpdate()
        {
            if (paused)
            {
                return;
            }
            PausableFixedUpdate();
        }

        public virtual void PausableFixedUpdate()
        {
            
        }
    }
}