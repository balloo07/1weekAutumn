using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleNextScene : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    private AudioSource audio;
    void Start()
    {
        Time.timeScale = 1f;
        audio = GetComponent<AudioSource>();
    }

    public void StartNextScene()
    {
        audio.Play();

        StartCoroutine(Checking(() => {
            SceneManager.LoadScene(_nextScene);
        }));
    }

    public delegate void functionType();
    private IEnumerator Checking(functionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audio.isPlaying)
            {
                callback();
                break;
            }
        }
    }
}
