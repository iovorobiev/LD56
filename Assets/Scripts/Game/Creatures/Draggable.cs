using System;
using System.Collections;
using Base;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace Game.Creatures
{
    public class Draggable : PausableBehaviour
    {
        private float speedBack = 1f;
        public Tilemap grid;
        public PowerButton powerButton;

        private int originalLayer;
        private Vector3 startPosition;
        private bool isDragging;
        private bool isActive;
        protected Animator _animator;

        protected void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void setDraggableActive(bool active)
        {
            isActive = active;
            originalLayer = gameObject.layer;
            startPosition = transform.position;
        }

        private void OnMouseEnter()
        {
            Debug.Log("Mouse enter");
        }

        private void OnMouseDown()
        {
            if (!isActive || powerButton.awaken) return;
            handleDragStart();
        }

        protected virtual void handleDragStart()
        {
            gameObject.layer = Layers.GHOST;
            isDragging = true;
            powerButton.RemoveCreature(gameObject);
            _animator.Play("carry");
        }

        private void OnMouseDrag()
        {
            if (!isDragging || !isActive) return;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

        private void OnMouseUp()
        {
            if (!isDragging || !isActive) return;
            var closestPos = grid.WorldToCell(transform.position);
            if (grid.HasTile(closestPos))
            {
                var layerMask = LayersHelper.GetAllLayersExcept(Layers.GHOST);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject);
                    StartCoroutine(FlyBack());
                }
                else
                {
                    SettleAtTile(grid.GetCellCenterWorld(closestPos));
                }
            }
            else
            {
                StartCoroutine(FlyBack()); 
            }
        }

        public virtual void SettleAtTile(Vector3 getCellCenterWorld)
        {
            isActive = true;
            isDragging = false;
            gameObject.layer = originalLayer;
            transform.position = getCellCenterWorld;
            powerButton.AddCreature(gameObject);
            _animator.Play("sleep");
        }

        IEnumerator FlyBack()
        {
            isActive = false;
            var time = 0f;
            while (time < 1f)
            {
                time += speedBack * Time.deltaTime;
                var position = Vector3.Lerp(transform.position, startPosition, time);
                transform.position = position;
                yield return new WaitForNextFrameUnit();
            }
            
            isActive = true;
            isDragging = false;
            gameObject.layer = originalLayer;
            _animator.Play("sleep");
        }
    }
}