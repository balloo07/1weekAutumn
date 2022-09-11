using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoad : MonoBehaviour
{
    private GameProfile _gameProfile;

    [SerializeField] private string _nextScene;
    [SerializeField] private bool _retry;

    private void Start()
    {
        _gameProfile = GameObject.Find("GameProfile").GetComponent<GameProfile>();
    }

    public void StartNextScene()
    {
        SceneManager.LoadScene(_nextScene);
        if (_retry)
        {
            _gameProfile._retry = true;
        }
    }
}
