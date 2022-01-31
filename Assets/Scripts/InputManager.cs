using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public enum Controls
    {
        Keyboard,
        Xbox,
        Playstation,
    }

    public event Action<InputValue> onMoveEvent;
    public event Action<InputValue> onRollEvent;
    public event Action onTopButtonEvent;
    public event Action onBottomButtonEvent;
    public event Action onLeftButtonEvent;
    public event Action onRightButtonEvent;
    public event Action<Controls> onControlsChangedEvent;

    private Controls _currentControls = Controls.Keyboard;
    public Controls CurrentControls
    {
        get => _currentControls;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnMove(InputValue value)
    {
        if (onMoveEvent != null)
        {
            onMoveEvent(value);
        }
    }

    void OnRoll(InputValue value)
    {
        if (onRollEvent != null)
        {
            onRollEvent(value);
        }
    }

    void OnTopButton()
    {
        if (onTopButtonEvent != null)
        {
            onTopButtonEvent();
        }
    }

    void OnBottomButton()
    {
        if (onBottomButtonEvent != null)
        {
            onBottomButtonEvent();
        }
    }

    void OnLeftButton()
    {
        if (onLeftButtonEvent != null)
        {
            onLeftButtonEvent();
        }
    }

    void OnRightButton()
    {
        if (onRightButtonEvent != null)
        {
            onRightButtonEvent();
        }
    }

    void OnExit()
    {
        Application.Quit();
    }

    void OnControlsChanged(PlayerInput input)
    {
        if (onControlsChangedEvent == null)
        {
            return;
        }

        if (input.currentControlScheme == "Keyboard")
        {
            _currentControls = Controls.Keyboard;
        }
        else if (input.currentControlScheme == "Gamepad")
        {
            Gamepad currentGamepad = UnityEngine.InputSystem.Gamepad.current;
            if (currentGamepad is UnityEngine.InputSystem.XInput.XInputController)
            {
                _currentControls = Controls.Xbox;

            }
            else if (currentGamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad)
            {
                _currentControls = Controls.Playstation;
            }
        }

        onControlsChangedEvent(CurrentControls);
    }
}
