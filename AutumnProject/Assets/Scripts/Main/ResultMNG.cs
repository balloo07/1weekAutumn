using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultMNG : MonoBehaviour
{
    private GameProfile _gameProfile;
    private StageState _stageState;

    [SerializeField] private Sprite[] _resultImages;
    [SerializeField] private AudioClip[] _resultBGM;
    
    [SerializeField] private GameObject _resultPopup;
    [SerializeField] private Image _resultImage;
    [SerializeField] private TextMeshProUGUI _resultText;

    [SerializeField] private GameObject _retryButton;
    [SerializeField] private GameObject _nextButton;

    private void Start()
    {
        _gameProfile = GameObject.Find("GameProfile").GetComponent<GameProfile>();
        _stageState = GameObject.Find("StageState").GetComponent<StageState>();
    }

    public void ResultShow()
    {
        _resultPopup.SetActive(true);
        var audioSorce = this.GetComponent<AudioSource>();

        var _score = _stageState._score;
        var _totalNotes = _stageState._totalNotes;
        
        if (_score == _totalNotes)
        {
            _stageState._gameResult = StageState.GameResult.Perfect;
            audioSorce.clip = _resultBGM[0];
            _resultImage.sprite = _resultImages[0];
            _resultText.text = "PERFECT!!!!!\n<size=23>すべてのアメを集めることができた";
        }
        else if (_score >= _totalNotes*0.8)
        {
            _stageState._gameResult = StageState.GameResult.Excellent;
            audioSorce.clip = _resultBGM[1];
            _resultImage.sprite = _resultImages[1];
            _resultText.text = "Excellent!!!\n<size=23>沢山のアメを集めることができた";
        }
        else if (_score >= _totalNotes*0.6)
        {
            _stageState._gameResult = StageState.GameResult.Great;
            audioSorce.clip = _resultBGM[2];
            _resultImage.sprite = _resultImages[2];
            _resultText.text = "GREAT!!\n<size=23>多くのアメを集めることができた";
        }
        else if (_score >= _totalNotes*0.4)
        {
            _stageState._gameResult = StageState.GameResult.Good;
            audioSorce.clip = _resultBGM[3];
            _resultImage.sprite = _resultImages[3];
            _resultText.text = "GOOD!\n<size=23>少しのアメを集めることができた";
        }
        else
        {
            _stageState._gameResult = StageState.GameResult.Bad;
            audioSorce.clip = _resultBGM[4];
            _resultImage.sprite = _resultImages[4];
            _resultText.text = "Oops...\n<size=23>もう一度チャレンジしてみよう";
            _nextButton.GetComponent<Button>().interactable = false;
        }

        _resultText.text += "\n<size=20><color=#777777>今まで集めたアメの合計："+_gameProfile._totalDropsCount+"コ";
        audioSorce.Play();
    }
}
