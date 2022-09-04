using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChargerMNG : MonoBehaviour
{
    // [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _speed = 1f;

    private float _startValue = 0f;
    private float _endValue = 1f;

    private bool _isMove = false;

    private float _startTime;
    private void Update()
    {
        DetectKeys ();

        if (_isMove)
        {
            var t = Mathf.Min(Time.time - _startTime, 1f);
            _slider.value = Mathf.Lerp(_startValue, _endValue, -t * (t-2) * _speed);
            
        }
    }
    
    private void DetectKeys(){
        if (Input.GetKeyDown (KeyCode.Space))
        {
            _startTime = Time.time;
            _isMove = true;
        }
    }

    public void updateValue(float goalValue)
    {
        _startValue = _slider.value;
        _endValue = goalValue;
        _isMove = true;
    }
}