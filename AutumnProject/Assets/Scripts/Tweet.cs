using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour
{
    private NotesGameController _gameController;
    private GameProfile _gameProfile;

    private void Start()
    {
        _gameController = GameObject.Find ("GameMNG").GetComponent<NotesGameController> ();
        _gameProfile = GameObject.Find ("GameProfile").GetComponent<GameProfile>();
    }

    public void GenerateTweet()
    {
        var _score = _gameController._score.ToString();
        var _totalScore = _gameController._totalNotes.ToString();
        var _result = _gameController._gameResult;
        var _totalDropsCount = _gameProfile._totalDropsCount;
        naichilab.UnityRoomTweet.Tweet ("rainydropsfall", "ミニゲーム「RainyDropsFall」\n"+
                                                       "結果は"+_result+"\n"+
                                                       "スコアは"+_score+"/"+_totalScore+"\n"+
                                                       "合計"+_totalDropsCount+"コのアメを集めました！\n"+
                                                       "#RainyDropsFalll");
    }
}
