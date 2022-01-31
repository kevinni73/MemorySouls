using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
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

    void OnControlsChanged(PlayerInput input)
    {
        if (onControlsChangedEvent == null)
        {
            return;
        }

        if (input.currentControlScheme == "Keyboard")
        {
            onControlsChangedEvent(Controls.Keyboard);
        } else if (input.currentControlScheme == "Gamepad")
        {
            Gamepad currentGamepad = UnityEngine.InputSystem.Gamepad.current;
            if (currentGamepad is UnityEngine.InputSystem.XInput.XInputController)
            {
                onControlsChangedEvent(Controls.Xbox);
            }
            else if (currentGamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad)
            {
                onControlsChangedEvent(Controls.Playstation);
            }
        }
    }
}
