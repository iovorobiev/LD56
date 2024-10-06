using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomMove : MonoBehaviour
{

    public float changeIntervalStart = 2f;
    public float changeIntervalEnd = 5f;

    public float speed;

    private Vector3 direction = Vector2.zero;

    private void Start()
    {
        StartCoroutine(changeDirection());
    }

    void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    IEnumerator changeDirection()
    {
        while (true)
        {
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f).normalized;
            Debug.Log("Changing direction!");
            yield return new WaitForSeconds(Random.Range(changeIntervalStart, changeIntervalEnd));    
        }
    }
}
