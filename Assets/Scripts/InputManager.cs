using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action<InputValue> onMoveEvent;
    public event Action<InputValue> onRollEvent;
    public event Action onTopButtonEvent;
    public event Action onBottomButtonEvent;
    public event Action onLeftButtonEvent;
    public event Action onRightButtonEvent;

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
}
