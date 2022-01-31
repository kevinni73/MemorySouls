using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource _source;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    // todo
}
