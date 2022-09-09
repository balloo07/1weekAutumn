using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class NotesGameController : MonoBehaviour
{
    //ゲームの進行状況（シーンをまたいで保存）
    private GameObject _gameProfile;
        
    public GameObject[] notes;
    private List<float> _timing = new List<float>();
    private List<int> _lineNum = new List<int>();
    public int _totalNotes;

    public string filePass;
    private int _notesCount = 0;

    private AudioSource _gameMusic;
    private float _startTime = 0;

    public float timeOffset = -1;

    public TextMeshProUGUI scoreText;
    public int _score = 0;
    
    [SerializeField] private GameObject _startText;
    [SerializeField] private GameObject _resultPopup;
    [SerializeField] private TextMeshProUGUI _resultText;

    [SerializeField] private AudioClip _perfectBGM;
    [SerializeField] private AudioClip _veryGoodBGM;
    [SerializeField] private AudioClip _goodBGM;
    [SerializeField] private AudioClip _ordinaryBGM;
    [SerializeField] private AudioClip _badBGM;

    public enum GameState
    {
        Prepare,
        MusicPlay,
        Pause,
        Result
    }
    public enum GameResult
    {
        Perfect,
        VeryGood,
        Good,
        Ordinary,
        Bad
    }
    
    public GameState _gameState;
    public GameResult _gameResult;
   
    void Start(){
        Time.timeScale = 1f;
        _gameState = GameState.Prepare;
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        LoadCSV ();
        _gameProfile = GameObject.Find("GameProfile");
    }

    void Update () {
        
        scoreText.text = "Score:  <color=#99dd44>" + _score.ToString () + "<color=#ffffff> / " + _totalNotes;

        //ゲームは開始しているか
        if (_gameState == GameState.Prepare)
        {
            if (Input.GetKey (KeyCode.Space)) {
                StartGame();
                _startText.SetActive(false);
                _gameState = GameState.MusicPlay;
            }
        }
        //音楽が流れているか
        if (_gameState == GameState.MusicPlay)
        {
            if (_notesCount < _totalNotes)
            {
                CheckNextNotes();
            }
            else
            {
                //すべてのノードが消えて判定が終わったあと
                Invoke(nameof(ShowResult), 2f);
                _gameState = GameState.Result;
            }
            // var _chargerMNG = GameObject.Find ("ChargerMNG").GetComponent<ChargerMNG>();
            // _chargerMNG.updateValue((float)_score/20f);
        }
    }

    private void ShowResult()
    {
        _resultPopup.SetActive(true);
        var audioSorce = this.GetComponent<AudioSource>();
        
        Time.timeScale = 0f;
        _gameMusic.Stop();
        if (_score == _totalNotes)
        {
            _gameResult = GameResult.Perfect;
            audioSorce.clip = _perfectBGM;
            
            Debug.Log("最高");
            _resultText.text = "PERFECT!";
        }
        else if (_score >= _totalNotes*0.8)
        {
            _gameResult = GameResult.VeryGood;
            audioSorce.clip = _veryGoodBGM;

            Debug.Log("とても良い");
            _resultText.text = "VERY GOOD!";
        }
        else if (_score >= _totalNotes*0.6)
        {
            _gameResult = GameResult.Good;
            audioSorce.clip = _goodBGM;

            Debug.Log("良い");
            _resultText.text = "GOOD!";
        }
        else if (_score >= _totalNotes*0.4)
        {
            _gameResult = GameResult.Ordinary;
            audioSorce.clip = _ordinaryBGM;

            Debug.Log("普通");
            _resultText.text = "ORDINARY";
        }
        else
        {
            _gameResult = GameResult.Bad;
            audioSorce.clip = _badBGM;

            Debug.Log("ダメかも");
            _resultText.text = "BAD...\nDont mind...";
        }
        audioSorce.Play();
    }

    public void StartGame(){
        _startTime = Time.time;
        _gameMusic.Play ();
        _gameState = GameState.MusicPlay;
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
            Quaternion.identity);
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
        _totalNotes = i;
    }

    float GetMusicTime(){
        return Time.time - _startTime;
    }

    public void GoodTimingFunc(int num){
        Debug.Log ("Line:" + num + " good!");
        Debug.Log (GetMusicTime());

        _score++;
    }
}
