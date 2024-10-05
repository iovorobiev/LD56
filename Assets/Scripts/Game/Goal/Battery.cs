using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Game;
using Unity.VisualScripting;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        var conductor = other.gameObject.GetComponent<ChargeConductor>();
        if (conductor.hasCharge())
        {
            PausableBehaviour.paused = true;
            Debug.Log("It's fixed!");
        }
    }
}
