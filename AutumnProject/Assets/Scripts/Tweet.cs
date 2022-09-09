using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour
{
    private NotesGameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find ("GameMNG").GetComponent<NotesGameController> ();
    }

    public void GenerateTweet()
    {
        var _score = _gameController._score.ToString();
        var _totalScore = _gameController._totalNotes.ToString();
        var _result = _gameController._gameResult;
        naichilab.UnityRoomTweet.Tweet ("rainydropsfall", "ミニゲーム「RainyDropsFall」で遊びました！\n"+
                                                       "結果は"+_result+"\n"+
                                                       "スコアは"+_score+"/"+_totalScore+"でした\n"+
                                                       "#RainyDropsFalll");
    }
}
