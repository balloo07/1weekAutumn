using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour
{
    private StageState _stageState;
    private GameProfile _gameProfile;

    private void Start()
    {
        _stageState = GameObject.Find ("StageState").GetComponent<StageState> ();
        _gameProfile = GameObject.Find ("GameProfile").GetComponent<GameProfile>();
    }

    public void GenerateTweet()
    {
        var score = _stageState._score.ToString();
        var totalScore = _stageState._totalNotes.ToString();
        var result = _stageState._gameResult.ToString().ToUpper();
        var totalDropsCount = _gameProfile._totalDropsCount;
        string stage;

        switch (_stageState._stage)
        {
            case 1:
                stage="雨漏りする棲家";
                break; 
            case 2:
                stage = "甘やかな街角";
                break;
            default:
                stage = "嵐の吹く夜に";
                break;
        }
        
        string resultText;
        naichilab.UnityRoomTweet.Tweet ("rainydropsfall", "ミニゲーム「RainyDropsFall」\n\n"+
                                                          "ステージ"+_stageState._stage+"："+stage+"\n"+
                                                          "スコア："+score+"/"+totalScore+"\n"+
                                                          result+"！\n\n"+
                                                          "合計"+totalDropsCount+"コのアメを集めました！\n"+
                                                          "#RainyDropsFall");
    }
}
