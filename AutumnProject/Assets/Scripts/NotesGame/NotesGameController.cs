using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class NotesGameController : MonoBehaviour {

    public GameObject[] notes;
    private List<float> _timing = new List<float>();
    private List<int> _lineNum = new List<int>();
    private int _totalNotes;

    public string filePass;
    private int _notesCount = 0;

    private AudioSource _audioSource;
    private float _startTime = 0;

    public float timeOffset = -1;

    private bool _isStarted = false;
    private bool _isPlaying = false;
    private bool _isFinished = false;
    public GameObject startButton;

    public TextMeshProUGUI scoreText;
    private int _score = 0;
    
    [SerializeField] private GameObject _startText;


    [SerializeField] private GameObject _resultPopup;
    [SerializeField] private TextMeshProUGUI _resultText;

    void Start(){
        _audioSource = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        LoadCSV ();
    }

    void Update () {
        
        scoreText.text = "Score:  <color=#99dd44>" + _score.ToString () + "<color=#ffffff> / " + _totalNotes;

        //ゲームは開始しているか
        if (!_isStarted)
        {
            if (Input.GetKey (KeyCode.Space)) {
                StartGame();
                _startText.SetActive(false);
                _isStarted = true;
            }
        }
        else
        {
            //音楽が流れているか
            if (_isPlaying) {
                if (_notesCount < _totalNotes)
                {
                    CheckNextNotes ();
                }
                else
                {
                    //すべてのノードが消えて判定が終わったあと
                    Invoke(nameof(ShowResult), 2f);
                    _isPlaying = false;
                }
                // var _chargerMNG = GameObject.Find ("ChargerMNG").GetComponent<ChargerMNG>();
                // _chargerMNG.updateValue((float)_score/20f);
            }            
        }
    }

    private void ShowResult()
    {
        _resultPopup.SetActive(true);
        // _audioSource.Stop();
        if (_score == _totalNotes)
        {
            Debug.Log("最高");
            _resultText.text = "PERFECT!";
        }
        else if (_score >= _totalNotes*0.8)
        {
            Debug.Log("とても良い");
            _resultText.text = "VERY GOOD!";
        }
        else if (_score >= _totalNotes*0.6)
        {
            Debug.Log("良い");
            _resultText.text = "GOOD!";
        }
        else if (_score >= _totalNotes*0.4)
        {
            Debug.Log("普通");
            _resultText.text = "ORDINARY";
        }
        else {
            Debug.Log("ダメかも");
            _resultText.text = "BAD...";
        }
        _isFinished = false;
    }

    public void StartGame(){
        _startTime = Time.time;
        _audioSource.Play ();
        _isPlaying = true;
    }

    void CheckNextNotes(){
        if (_timing [_notesCount] + timeOffset < GetMusicTime () && _timing [_notesCount] != 0) {
            SpawnNotes (_lineNum[_notesCount]);
            _notesCount++;
        }
    }

    void SpawnNotes(int num){
        Instantiate (notes[num], 
            new Vector3 (-4.0f + (2.0f * num), 10.0f, 0),
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
