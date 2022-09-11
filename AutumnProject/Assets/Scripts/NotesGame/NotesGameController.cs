using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using TMPro;

public class NotesGameController : MonoBehaviour
{
    private StageState _stageState;
    
    public GameObject[] notes;
    private List<float> _timing = new List<float>();
    private List<int> _lineNum = new List<int>();

    public string filePass;
    private int _notesCount = 0;

    private AudioSource _gameMusic;
    private float _startTime = 0;

    public float timeOffset = -1;

    public TextMeshProUGUI scoreText;
    public int _score = 0;
    
    [SerializeField] private GameObject _startText;

    private void Start(){
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        _stageState = GameObject.Find("StageState").GetComponent<StageState>();
        LoadCSV ();
    }

    private void Update () {
        
        scoreText.text = "Score:  <color=#99dd44>" + _score.ToString () + "<color=#ffffff> / " + _stageState._totalNotes;

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
                CheckNextNotes();
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
        _stageState._score = _score;
        this.GetComponent<ResultMNG>().ResultShow();
    }

    public void StartGame(){
        _startTime = Time.time;
        _gameMusic.Play ();
    }

    void CheckNextNotes(){
        if (_timing [_notesCount] + timeOffset < GetMusicTime () && _timing [_notesCount] != 0) {
            SpawnNotes (_lineNum[_notesCount]);
            _notesCount++;
        }
    }

    void SpawnNotes(int num)
    {
        var interval = 2.8f;    //ノート同士の間隔

        Instantiate (notes[num], 
            new Vector3 (-2*interval + (interval * num), 10.0f, 0),
            Quaternion.identity);;
    }

    void LoadCSV(){
        int i = 0;
        TextAsset csv = Resources.Load (filePass) as TextAsset;
        StringReader reader = new StringReader (csv.text);

        while (reader.Peek () > -1) {

            string line = reader.ReadLine ();
            string[] values = line.Split (',');
                _timing.Add(float.Parse( values [0] ));
                _lineNum.Add(int.Parse( values [1] ));
                i++;
        }
        _stageState._totalNotes = i;
    }

    float GetMusicTime(){
        return Time.time - _startTime;
    }

    public void GoodTimingFunc(int num){
        // Debug.Log ("Line:" + num + " good!");
        // Debug.Log (GetMusicTime());

        _score++;
    }
}
