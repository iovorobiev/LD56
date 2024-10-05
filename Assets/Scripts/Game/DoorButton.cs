using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public List<Door> doorsUnderControl;

    public void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var door in doorsUnderControl)
        {
            door.ToggleOpen();
        }
    }
}
