using TMPro;
using UnityEngine;

public class Victory : MonoBehaviour
{
    TextMeshProUGUI _text;
    AudioSource _audio;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _audio = GetComponent<AudioSource>();
    }

    public void Enable()
    {
        _text.enabled = true;
        _audio.Play();
    }
}
