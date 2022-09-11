using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using Random = System.Random;

public class RandomGenerator : MonoBehaviour
{
    private StageState _stageState;
    
    [SerializeField] private GameObject[] notes;
    [SerializeField] private GameObject[] gomiNotes;

    private int _notesCount = 0;

    private AudioSource _gameMusic;

    private float _time = 0;    //経過時間
    private float _intervalTime;
    
    [SerializeField] private int _missNoteRate;
    //時間間隔の最小値
    [SerializeField] private float minTime;
    //時間間隔の最大値
    [SerializeField] private float maxTime;

    [SerializeField] private int _totalDropsCount=30;
    [SerializeField] private GameObject _startText;

    private void Start(){
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        _stageState = GameObject.Find("StageState").GetComponent<StageState>();
        _stageState._totalNotes = _totalDropsCount;
    }

    private void Update ()
    {
        _time += Time.deltaTime;
        
        //ゲームは開始しているか
        if (_stageState._gameState == StageState.GameState.Prepare)
        {
            if (Input.GetKey (KeyCode.Space)) {
                StartGame();
                _startText.SetActive(false);
                _stageState._gameState = StageState.GameState.MusicPlay;
            }
        }
        //音楽が流れているか
        if (_stageState._gameState == StageState.GameState.MusicPlay)
        {
            if (_notesCount < _stageState._totalNotes)
            {
                if (_time > _intervalTime)
                {
                    SpawnNotes();
                    _time = 0f;
                    _intervalTime = GetRandomTime();
                }
            }
            else
            {
                //すべてのノードが消えて判定が終わったあと
                Invoke(nameof(ShowResult), 2f);
                _stageState._gameState = StageState.GameState.Result;
            }
        }
    }
    private void ShowResult()
    {
        Time.timeScale = 0f;
        _gameMusic.Stop();
        this.GetComponent<ResultMNG>().ResultShow();
    }

    public void StartGame(){
        _gameMusic.Play ();
    }

    private void SpawnNotes()
    {
        var interval = 2.8f;    //ノート同士の間隔
        var num = UnityEngine.Random.Range(0, 5);
        var randomGomi = UnityEngine.Random.Range(0,_missNoteRate);
        
        if (randomGomi==0)
        {
            Instantiate (gomiNotes[num],
                new Vector3 (-2*interval + (interval * num), 10.0f, 0),
                Quaternion.identity);
        }
        else
        {
            Instantiate (notes[num], 
                new Vector3 (-2*interval + (interval * num), 10.0f, 0),
                Quaternion.identity);
            _notesCount++;            
        }

    }
    
    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return UnityEngine.Random.Range(minTime, maxTime);
    }
}
