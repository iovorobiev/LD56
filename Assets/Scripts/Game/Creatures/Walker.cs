using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Game;
using Game.Creatures;
using UnityEngine;

public class Walker : Carriable, BullTrigger, ChargeConductor, Awakable
{
    public float speed;
    public Vector3 direction;
    public bool triggersBull;

    public Mode currentMode = Mode.SLEEP;
    private GameObject charge;

    private Vector3 originalPosition;

    private void Start()
    {
        base.Start();
        originalPosition = transform.position;
        if (currentMode == Mode.AWAITING)
        {
            setDraggableActive(true);
        }
    }

    public override void PausableUpdate()
    {
        base.PausableUpdate();
        if (currentMode != Mode.WALKING)
        {
            return;
        }
        transform.position += direction * (speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        Debug.Log("Collision detected");
        if (other.gameObject.layer == Layers.WALL || other.gameObject.layer == Layers.CREATURES || other.gameObject.layer == Layers.BULL)
        {
            Debug.Log("With wall");
            direction *= -1;
            if (direction == Vector3.left || direction == Vector3.right)
            {
                transform.localScale = new Vector3(
                    transform.localScale.x * -1f, 
                    transform.localScale.y, 
                    transform.localScale.z);    
            }
            
        }
        
    }

    public override void takenByBull(Bull carryingBull)
    {
        base.takenByBull(carryingBull);
        currentMode = Mode.CARRIED;
        _animator.Play("carry");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.CHARGE && charge == null)
        {
            takeCharge(other.gameObject);
        }
    }

    private void takeCharge(GameObject gameObject)
    {
        Charge comp = gameObject.GetComponent<Charge>();
        if (comp.taken)
        {
            return;
        }
        charge = gameObject;
        charge.transform.parent = transform;
        charge.transform.position = transform.position + Vector3.up / 2f;
        comp.take();
    }

    public bool doesTriggerBull()
    {
        return triggersBull;
    }

    public override void releasedByBull()
    {
        base.releasedByBull();
        currentMode = Mode.WALKING;
        _animator.Play("walk");
    }

    public enum Mode
    {
        WALKING,
        IDLE,
        CARRIED,
        SLEEP,
        AWAITING,
    }

    public bool hasCharge()
    {
        return charge != null;
    }

    public void AwakeAndWork()
    {
        base.AwakeAndWork();
        Debug.Log("Awaking!");
        currentMode = Mode.WALKING;
        _animator.Play("walk");
    }

    public void ResetPositionAndState()
    {
        base.ResetPositionAndState();
        currentMode = Mode.SLEEP;
        charge = null;
        transform.position = originalPosition;
    }
}
