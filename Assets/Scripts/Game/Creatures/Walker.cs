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

    private Mode currentMode = Mode.SLEEP;
    private GameObject charge;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
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
        if (other.gameObject.layer == Layers.WALL || other.gameObject.layer == Layers.CREATURES)
        {
            direction *= -1;    
        }

        if (other.gameObject.layer == Layers.BULL)
        {
            currentMode = Mode.CARRIED;
        }
        
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
    }

    enum Mode
    {
        WALKING,
        IDLE,
        CARRIED,
        SLEEP,
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
    }

    public void ResetPositionAndState()
    {
        base.ResetPositionAndState();
        currentMode = Mode.SLEEP;
        charge = null;
        transform.position = originalPosition;
    }
}
