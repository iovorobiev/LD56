using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;

public class Door : PausableBehaviour
{
    private BoxCollider2D collider2D;
    private Animator _animator;
    public bool isOpened;
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    public void ToggleOpen()
    {
        if (isOpened)
        {
            closeDoor();
        }
        else
        {
            openDoor();
        }
    }

    private void openDoor()
    {
        collider2D.enabled = false;
        Debug.Log("Door opening " + collider2D.enabled);
        _animator.Play("DoorOpening");
        isOpened = true;
    }

    private void closeDoor()
    {
        collider2D.enabled = true;
        Debug.Log("Door Closing " + collider2D.enabled);
        _animator.Play("DoorClosed");
        isOpened = false;
    }
    
    
}
