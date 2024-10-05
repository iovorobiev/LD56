using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Base;
using UnityEngine;

public class DoorButton : PausableBehaviour
{
    public List<Door> doorsUnderControl;
    private float cooldown = 0.3f;
    private float currentCooldown = 0f;

    public override void PausableUpdate()
    {
        base.PausableUpdate();
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (currentCooldown > 0f)
        {
            return;
        }
        
        foreach (var door in doorsUnderControl)
        {
            Debug.Log("Toggle door");
            door.ToggleOpen();
        }

        currentCooldown = cooldown;
    }
}
