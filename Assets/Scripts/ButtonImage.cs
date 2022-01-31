using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImage : MonoBehaviour
{
    Image _image;

    [SerializeField] Sprite _xboxSprite;
    [SerializeField] Sprite _keyboardSprite;
    [SerializeField] Sprite _playstationSprite;

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
