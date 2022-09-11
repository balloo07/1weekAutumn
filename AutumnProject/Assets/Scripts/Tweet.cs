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
        var score = _gameController._score.ToString();
        var totalScore = _gameController._totalNotes.ToString();
        var result = _gameController._gameResult.ToString().ToUpper();
        var totalDropsCount = _gameProfile._totalDropsCount;
        
        string resultText;
        naichilab.UnityRoomTweet.Tweet ("rainydropsfall", "ミニゲーム「RainyDropsFall」\n"+
                                                          "結果は"+result+"!\n"+
                                                          "スコアは"+score+"/"+totalScore+"\n"+
                                                          "合計"+totalDropsCount+"コのアメを集めました！\n"+
                                                          "#RainyDropsFall");
    }
}
