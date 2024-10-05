using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PauseManager : MonoBehaviour
{
    public GameControls controls;
    public GameObject pauseMenu;
    private InputAction pauseAction;

    private void Awake()
    {
        controls = new GameControls();
    }

    private void Start()
    {
        pauseAction.performed += TogglePause;
    }

    private void OnEnable()
    {
        if (controls == null)
        {
            Debug.Log("Controls null");
            return;
        }
        pauseAction = controls.Game.Menu;
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    void TogglePause(InputAction.CallbackContext context)
    {
        if (PausableBehaviour.paused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        PausableBehaviour.paused = true;
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        PausableBehaviour.paused = false;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        StartCoroutine(SceneLoader.LoadSceneAsync(0));
    }
}
