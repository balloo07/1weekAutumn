using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPopup : MonoBehaviour
{
    private StageState _stageState;

    private void Start()
    {
        _stageState = GameObject.Find("StageState").GetComponent<StageState>();
    }

    public void IntroClicked()
    {
        Time.timeScale = 1f;
        _stageState._gameState = StageState.GameState.Prepare;
        var audio = this.GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        Destroy(transform.GetChild(0).gameObject);
    }
}
