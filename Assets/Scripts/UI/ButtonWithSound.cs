using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithSound : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        _source.Play();
    }
}
