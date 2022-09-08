using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    [SerializeField] private GameObject popupMenu;
    private AudioSource _gameMusic;
    private AudioSource _menuSE;
    
    void Start()
    {
        _menuSE = GetComponent<AudioSource>();
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
    }

    public void MenuActive()
    {
        _menuSE.Play();
        _gameMusic.Pause();
        Time.timeScale = 0f;
        popupMenu.SetActive(true);
    }
    
    public void MenuHide()
    {
        _menuSE.Play();
        _gameMusic.Play();
        Time.timeScale = 1f;
        popupMenu.SetActive(false);
    }

    public delegate void functionType();
    private IEnumerator Checking(functionType callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!GetComponent<AudioSource>().isPlaying)
            {
                callback();
                break;
            }
        }
    }
}
