using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressMaterialAnimator : MonoBehaviour
{
    public float speed = 1f;
    public float pauseBetweenRuns = 0f;
    public bool animateOnStart = true;
    public bool inifinite = true;
    public bool reverse = false;

    private Material _material;
    private float currentProgress = 0f;

    private Coroutine activeJob;

    private void Start()
    {
        _material = GetMaterial();
        if (animateOnStart)
        {
            StartAnimating(inifinite);
        }
    }

    public virtual Material GetMaterial()
    {
        return GetComponent<Renderer>().material;
    }

    public void StartAnimating(bool infinite)
    {
        activeJob = StartCoroutine(animate(infinite));
    }

    public void StopAnimating()
    {
        StopCoroutine(activeJob);
        currentProgress = 0f;
        setProgress(getProgress());
    }

    private float getProgress()
    {
        if (reverse)
        {
            return 1 - currentProgress;
        }

        return currentProgress;
    }
    
    IEnumerator animate(bool inifinite)
    {
        while (true)
        {
            currentProgress += Time.deltaTime * speed;
            setProgress(getProgress());
            if (currentProgress >= 1f)
            {
                currentProgress = 0f;
                if (!inifinite)
                {
                    yield break;
                }
                yield return new WaitForSeconds(pauseBetweenRuns);
            }

            yield return new WaitForNextFrameUnit();
        }
    }

    private void setProgress(float p)
    {
        _material.SetFloat("_Progress", p);
        
    }
}
