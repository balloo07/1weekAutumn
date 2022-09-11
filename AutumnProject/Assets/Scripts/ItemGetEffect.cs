using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] float pitchRange = 0.1f;
    
    private AudioSource _audioSource;
    private AudioClip _clip;
    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _clip = _audioSource.clip;

        _audioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        _audioSource.PlayOneShot(_clip);
        Destroy(gameObject, _lifeTime);
    }
}
