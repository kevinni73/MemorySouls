using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer _renderer;

    [SerializeField] Sprite _xboxSprite;
    [SerializeField] Sprite _xboxAltSprite;

    [SerializeField] Sprite _keyboardSprite;
    [SerializeField] Sprite _keyboardAltSprite;

    [SerializeField] Sprite _playstationSprite;
    [SerializeField] Sprite _playstationAltSprite;

    Sprite _normalSprite;
    Sprite _altSprite;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        FindObjectOfType<InputManager>().onControlsChangedEvent += onControlsChanged;
        onControlsChanged(InputManager.Controls.Keyboard);
    }

    private void onControlsChanged(InputManager.Controls controls)
    {
        switch(controls)
        {
            case InputManager.Controls.Keyboard:
                _normalSprite = _keyboardSprite;
                _altSprite = _keyboardAltSprite;
                break;
            case InputManager.Controls.Xbox:
                _normalSprite = _xboxSprite;
                _altSprite = _xboxAltSprite;
                break;
            case InputManager.Controls.Playstation:
                _normalSprite = _playstationSprite;
                _altSprite = _playstationAltSprite;
                break;
        }

        _renderer.sprite = _normalSprite;
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
        _renderer.sprite = _normalSprite;
        _renderer.color = Color.white;
    }

    public void Deselect()
    {
        _renderer.sprite = _normalSprite;
        _renderer.color = new Color(0.3f, 0.3f, 0.3f, 1);
    }

    public void Incorrect()
    {
        _renderer.sprite = _normalSprite;
        _renderer.color = Color.red;
    }

    public void Correct()
    {
        _renderer.sprite = _altSprite;
    }
}
