using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private StageState _stageState;

    [SerializeField] private GameObject popupMenu;
    private AudioSource _gameMusic;
    private AudioSource _menuSE;
    
    void Start()
    {
        _stageState = GameObject.Find("StageState").GetComponent<StageState>();
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        _menuSE = GetComponent<AudioSource>();
    }

    public void MenuActive()
    {
        var state = _stageState._gameState;
        //イントロと結果画面では無反応
        if (state == StageState.GameState.Intro || state == StageState.GameState.Result) return;
        _menuSE.Play();
        if (state==StageState.GameState.MusicPlay)
        {
            _gameMusic.Pause();
        } 
        Time.timeScale = 0f;
        popupMenu.SetActive(true);
    }
    
    public void MenuHide()
    {
        _menuSE.Play();
        if (_stageState._gameState == StageState.GameState.MusicPlay)
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
