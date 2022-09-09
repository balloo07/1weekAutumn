using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DG.Tweening.DOTween;
using Random = UnityEngine.Random;

public class EnemyGhost : MonoBehaviour
{
    [SerializeField] private float _span = 6f;
    private float _currentTime;
    
    private float _direction = 1f;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_span < _currentTime)
        {
            _currentTime = 0f;
            _direction *= -1f;
            this.transform.Rotate (0f, 180f, 0f);
        }
        this.transform.position += _direction * Vector3.left * Time.deltaTime * 0.5f;
        this.transform.position += Vector3.up * Mathf.Sin(Time.time)*Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color += new Color(0f,0f,0f,-Mathf.Sin(Time.time)*Time.deltaTime);
    }
}
