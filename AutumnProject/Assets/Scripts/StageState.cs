using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageState : MonoBehaviour
{
    private GameProfile _gameProfile;

    [SerializeField] private GameObject _introPopup;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private int _stage;
    public GameState _gameState;
    public GameResult _gameResult;
    public int _totalNotes=0;
    public int _score=0;
    
    public enum GameState
    {
        Intro,
        Prepare,
        MusicPlay,
        Pause,
        Result
    }
    public enum GameResult
    {
        Perfect,
        Excellent,
        Great,
        Good,
        Bad,
        NotFinished
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _gameProfile = GameObject.Find("GameProfile").GetComponent<GameProfile>();

        //リトライのときはイントロを飛ばす
        if (_gameProfile._retry)
        {
            _gameState = GameState.Prepare;
            Time.timeScale = 1f;
        }
        else
        {
            _gameState = GameState.Intro;
            _introPopup.SetActive(true);
            Time.timeScale = 0f;
        }
        _gameResult = GameResult.NotFinished;
    }

    private void Update()
    {
        _scoreText.text = "Score:  <color=#99dd44>" + _score.ToString () + "<color=#ffffff> / " + _totalNotes;
    }
}
