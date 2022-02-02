using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonImage : MonoBehaviour
{
    [SerializeField] Sprite _xboxSprite;
    [SerializeField] Sprite _keyboardSprite;
    [SerializeField] Sprite _playstationSprite;

    Image _image;

    InputManager Input;

    void Awake()
    {
        _image = GetComponent<Image>();

        Input = FindObjectOfType<InputManager>();
        Input.onControlsChangedEvent += onControlsChanged;
        onControlsChanged(Input.CurrentControls);
    }

    void OnDestroy()
    {
        Input.onControlsChangedEvent -= onControlsChanged;
    }

    private void onControlsChanged(InputManager.Controls controls)
    {
        switch (controls)
        {
            case InputManager.Controls.Keyboard:
                _image.sprite = _keyboardSprite;
                break;
            case InputManager.Controls.Xbox:
                _image.sprite = _xboxSprite;
                break;
            case InputManager.Controls.Playstation:
                _image.sprite = _playstationSprite;
                break;
        }
    }
}
