using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InfoShower : MonoBehaviour
{

    public PageSwitcher pageSwitcher;

    private void OnMouseEnter()
    {
        pageSwitcher.ShowPage(4);
    }

    private void OnMouseExit()
    {
        pageSwitcher.ShowDefault();
    }
}
