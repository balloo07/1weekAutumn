using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private NotesGameController _gameController;

    [SerializeField] private GameObject popupMenu;
    private AudioSource _gameMusic;
    private AudioSource _menuSE;
    
    void Start()
    {
        _gameController = GameObject.Find ("GameMNG").GetComponent<NotesGameController> ();
        _menuSE = GetComponent<AudioSource>();
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
    }

    public void MenuActive()
    {
        var state = _gameController._gameState;
        if (state == NotesGameController.GameState.Result) return;
        _menuSE.Play();
        if (state==NotesGameController.GameState.MusicPlay)
        {
            _gameMusic.Pause();
        } 
        Time.timeScale = 0f;
        popupMenu.SetActive(true);
    }
    
    public void MenuHide()
    {
        _menuSE.Play();
        if (_gameController._gameState == NotesGameController.GameState.MusicPlay)
        {
            _gameMusic.Play();
        }
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
