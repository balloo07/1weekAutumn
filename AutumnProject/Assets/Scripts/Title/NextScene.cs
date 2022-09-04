using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void StartNextScene()
    {
        audio.Play();

        StartCoroutine(Checking(() => {
            SceneManager.LoadScene("GameScene");
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
