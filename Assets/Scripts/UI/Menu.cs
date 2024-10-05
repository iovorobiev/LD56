using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class Menu : MonoBehaviour
{
    public Button start;
    public Button quit;
    
    public void StartGame()
    {
        start.enabled = false;
        quit.enabled = false;
        StartCoroutine(SceneLoader.LoadSceneAsync(1));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
