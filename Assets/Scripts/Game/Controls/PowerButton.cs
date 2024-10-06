using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using Utils;

public class PowerButton : PausableBehaviour
{
    public List<GameObject> creatures;

    public bool awaken;
    private float clickCooldown;
    private float clickAllowedInterval = 0.3f;
    public Battery battery;

    public override void PausableUpdate()
    {
        base.PausableUpdate();
        if (clickCooldown < clickAllowedInterval)
        {
            clickCooldown += Time.deltaTime;
            return;
        }
        if (DetectClick())
        {
            toggleAwake();
        }
    }

    private void toggleAwake()
    {
        if (awaken)
        {
            StartCoroutine(SceneLoader.LoadSceneAsync(battery.currentLevel));
            return;
        }
        foreach (var creature in creatures)
        {
            var awakable = creature.GetComponent<Awakable>();
            if (awaken)
            {
                Debug.Log("ResettingPosition");
                awakable.ResetPositionAndState();
            }
            else
            {
                Debug.Log("AwakingChildren");
                awakable.AwakeAndWork();
            }
        }

        awaken = !awaken;
    }

    public void AddCreature(GameObject creature)
    {
        creatures.Add(creature);
    }

    public void RemoveCreature(GameObject creature)
    {
        creatures.Remove(creature);
    }

    public bool DetectClick()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            clickCooldown = 0f;
            return true;
        }

        return false;
    }
}
