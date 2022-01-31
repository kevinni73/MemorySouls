using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLooper : MonoBehaviour
{
    [SerializeField] AudioClip _startClip;
    [SerializeField] AudioClip _loopClip;

    AudioSource _source;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.loop = true;
        StartCoroutine(playAudio());
    }

    IEnumerator playAudio()
    {
        _source.clip = _startClip;
        _source.Play();
        yield return new WaitForSeconds(_source.clip.length);
        _source.clip = _loopClip;
        _source.Play();
    }
}
