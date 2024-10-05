using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class Charge : MonoBehaviour, Awakable
{
    public bool taken;
    private Vector3 originalPosition;
    private Transform originalParent;

    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public void take()
    {
        taken = true;
    }

    public void AwakeAndWork()
    {
        // Do nothing
    }

    public void ResetPositionAndState()
    {
        taken = false;
        transform.parent = originalParent;
        transform.position = originalPosition;
    }
}
