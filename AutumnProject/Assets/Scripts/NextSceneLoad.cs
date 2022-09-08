using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoad : MonoBehaviour
{
    [SerializeField] private string _nextScene;

    public void StartNextScene()
    {
        SceneManager.LoadScene(_nextScene);
    }
}
