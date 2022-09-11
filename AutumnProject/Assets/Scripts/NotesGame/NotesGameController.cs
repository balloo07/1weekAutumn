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
    private GameProfile _gameProfile;
        
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
    [SerializeField] private Image _resultImage;
    [SerializeField] private TextMeshProUGUI _resultText;

    [SerializeField] private Sprite _perfectImage;
    [SerializeField] private Sprite _excellentImage;
    [SerializeField] private Sprite _greatImage;
    [SerializeField] private Sprite _goodImage;
    [SerializeField] private Sprite _badImage;

    [SerializeField] private AudioClip _perfectBGM;
    [SerializeField] private AudioClip _excellentBGM;
    [SerializeField] private AudioClip _greatBGM;
    [SerializeField] private AudioClip _goodBGM;
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
        Excellent,
        Great,
        Good,
        Bad
    }
    
    public GameState _gameState;
    public GameResult _gameResult;
   
    void Start(){
        Time.timeScale = 1f;
        _gameState = GameState.Prepare;
        _gameMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
        LoadCSV ();
        _gameProfile = GameObject.Find("GameProfile").GetComponent<GameProfile>();
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
            
            Debug.Log("完璧");
            _resultImage.sprite = _perfectImage;
            _resultText.text = "PERFECT!!!!!\n<size=23>すべてのアメを集めることができた";
        }
        else if (_score >= _totalNotes*0.8)
        {
            _gameResult = GameResult.Excellent;
            audioSorce.clip = _excellentBGM;

            Debug.Log("素晴らしい");
            _resultImage.sprite = _excellentImage;
            _resultText.text = "Excellent!!!\n<size=23>沢山のアメを集めることができた";
        }
        else if (_score >= _totalNotes*0.6)
        {
            _gameResult = GameResult.Great;
            audioSorce.clip = _greatBGM;

            Debug.Log("良い調子");
            _resultImage.sprite = _greatImage;
            _resultText.text = "GREAT!!\n<size=23>多くのアメを集めることができた";
        }
        else if (_score >= _totalNotes*0.4)
        {
            _gameResult = GameResult.Good;
            audioSorce.clip = _goodBGM;

            Debug.Log("クリア");
            _resultImage.sprite = _goodImage;
            _resultText.text = "GOOD!\n<size=23>少しのアメを集めることができた";
        }
        else
        {
            _gameResult = GameResult.Bad;
            audioSorce.clip = _badBGM;

            Debug.Log("もう一度");
            _resultImage.sprite = _badImage;
            _resultText.text = "Oops...\n<size=23>このままではアメびたしになってしまう\nもう一度チャレンジしてみよう";
        }

        _resultText.text += "\n<size=20><color=#777777>今まで集めたアメの合計："+_gameProfile._totalDropsCount+"コ";
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
