using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Game;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Battery : MonoBehaviour
{
    public GameObject winDialog;
    public int currentLevel;

    public void ShowWin()
    {
        PausableBehaviour.paused = true;
        winDialog.SetActive(true);
    }

    public void NextLevel()
    {
        StartCoroutine(SceneLoader.LoadSceneAsync(currentLevel++));
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        var conductor = other.gameObject.GetComponent<ChargeConductor>();
        if (conductor.hasCharge())
        {
            ShowWin();
        }
    }
}
