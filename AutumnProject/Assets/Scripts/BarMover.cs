using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMover : MonoBehaviour
{
    [SerializeField] private bool _barMove = false;
    private float _startTime = 0f;
    
    private void Start()
    {
        _startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_barMove)
        {
            this.transform.position += Vector3.up * Mathf.Sin(Time.time-_startTime)*Time.deltaTime;
        }
    }
}
