using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMover : MonoBehaviour
{
    [SerializeField] private bool _barMove = false;

    // Update is called once per frame
    void Update()
    {
        if (_barMove)
        {
            this.transform.position += Vector3.up * Mathf.Sin(Time.time)*Time.deltaTime;
        }
    }
}
