using Base;
using UnityEngine;

namespace Game.Creatures
{
    public class Draggable : PausableBehaviour
    {
        public bool DetectClick()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return false;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }

            return false;
        }
    }
}