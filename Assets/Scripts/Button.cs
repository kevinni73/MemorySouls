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

    public void Disable()
    {
        _renderer.enabled = false;
    }

    public void Enable()
    {
        _renderer.enabled = true;
    }

    public void Select()
    {
        _renderer.sprite = _xboxSprite;
        _renderer.color = Color.white;
    }

    public void Deselect()
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
