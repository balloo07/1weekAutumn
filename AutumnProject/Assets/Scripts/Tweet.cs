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
        var _result = _gameController._gameResult;
        naichilab.UnityRoomTweet.Tweet ("balloondemo", "ツイートサンプルです。\n"+
                                                       "結果は"+_result+"\n"+
                                                       "スコアは"+_score+"でした\n"+
                                                       "#RainyDropsFalll");
    }
}
