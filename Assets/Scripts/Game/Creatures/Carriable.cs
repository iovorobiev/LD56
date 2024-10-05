using System;
using Base;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Creatures
{
    public class Carriable : Draggable, Awakable
    {
        private Bull _bull;
        private Transform originalParent;
        
        public virtual void takenByBull(Bull carryingBull)
        {
            Debug.Log("Taken by bull " + carryingBull);
            _bull = carryingBull;
            originalParent = transform.parent;
            transform.parent = carryingBull.transform;
        }

        public virtual void releasedByBull()
        {
            _bull = null;
            transform.parent = originalParent;
            var closestPos = grid.WorldToCell(transform.position);
            transform.position =  grid.GetCellCenterWorld(closestPos);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == Layers.BULL)
            {
                return;
            }
            if (_bull != null)
            {
                
                _bull.onCarriedMetCollision();
            }
        }


        public virtual void AwakeAndWork()
        {
            // Do nothing
        }

        public virtual void ResetPositionAndState()
        {
            Debug.Log("Resetting pos and state");
            transform.parent = originalParent;
            _bull = null;
        }
    }
}