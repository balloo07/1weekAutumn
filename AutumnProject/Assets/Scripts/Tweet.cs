using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour
{
    public void GenerateTweet()
    {
        naichilab.UnityRoomTweet.Tweet ("balloondemo", "ツイートサンプルです。\n"+
        "#ハカセくんのあやしい実験室");
    }
}
