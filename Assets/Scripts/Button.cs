using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer _renderer;

    [SerializeField] Sprite _xboxSprite;
    [SerializeField] Sprite _xboxAltSprite;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetRendererColor(Color color)
    {
        _renderer.color = color;
    }

    public void Enable()
    {
        _renderer.sprite = _xboxSprite;
        _renderer.color = Color.white;
    }

    public void Disable()
    {
        _renderer.sprite = _xboxSprite;
        _renderer.color = Color.gray;
    }

    public void Incorrect()
    {
        _renderer.sprite = _xboxSprite;
        _renderer.color = Color.red;
    }

    public void Correct()
    {
        _renderer.sprite = _xboxAltSprite;
    }
}
