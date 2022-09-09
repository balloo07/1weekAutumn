using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private void Start()
    {
        //スライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);
    }

    //BGM
    private void SetAudioMixerBGM(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f,-80f,0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM",volume);
        bgmSlider.GetComponent<AudioSource>().Play();
        // Debug.Log($"BGM:{volume}");
    }

    //SE
    private void SetAudioMixerSE(float value)
    {
        //5段階補正
        value /= 5;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f,-80f,0f);
        //audioMixerに代入
        audioMixer.SetFloat("SE",volume);
        seSlider.GetComponent<AudioSource>().Play();
        // Debug.Log($"SE:{volume}");
    }
}