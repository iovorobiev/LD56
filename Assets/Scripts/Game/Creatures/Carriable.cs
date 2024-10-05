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
                return;
            }
        }


        public virtual void AwakeAndWork()
        {
            Debug.Log("Carriable awaking");
            // Do nothing
        }

        public virtual void ResetPositionAndState()
        {
            transform.parent = originalParent;
            _bull = null;
        }
    }
}