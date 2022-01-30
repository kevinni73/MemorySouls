using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtons : MonoBehaviour
{
    int _comboSize = 4;
    List<int> _combo = new List<int>();
    int _comboIndex = 0;

    IndicatorManager Indicators;
    List<SpriteRenderer> _childRenderers;

    [SerializeField] Button TopButton;
    [SerializeField] Button BottomButton;
    [SerializeField] Button LeftButton;
    [SerializeField] Button RightButton;
    List<Button> _buttons;

    [SerializeField] Enemy _enemy;

    [SerializeField] float _resetTime = 0.05f;
    float _resetTimer;

    [SerializeField] float _correctFlashTime = 0.05f;
    float _correctFlashTimer;

    private enum ButtonIndex
    {
        Top,
        Bottom,
        Left,
        Right,
    }
    Queue<ButtonIndex> _buttonPresses = new Queue<ButtonIndex>();

    bool _enabled;


    void Awake()
    {
        Indicators = GetComponentInChildren<IndicatorManager>();
        Indicators.Init(_comboSize);

        // For convenient accessing buttons by index, make sure it matches ButtonIndex order
        _buttons = new List<Button> { TopButton, BottomButton, LeftButton, RightButton };
    }

    private void Start()
    {
        _childRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        ChangeEnabled(false);

        GenerateCombo();
    }

    void Update()
    {
        if (!_enabled)
        {
            return;
        }

        if (_resetTimer > 0)
        {
            _resetTimer -= Time.deltaTime;
            if (_resetTimer <= 0)
            {
                Debug.Log("Reset Triggered");
                ChangeEnabled(true);
            }

            return;
        }

        if (_correctFlashTimer > 0)
        {
            _correctFlashTimer -= Time.deltaTime;
            if (_correctFlashTimer <= 0)
            {
                Debug.Log("Correct flash triggered");
                _buttons[_combo[_comboIndex - 1]].Disable();
                _buttons[_combo[_comboIndex]].Enable();
            }

            return;
        }

        while (_buttonPresses.Count > 0)
        {
            if (_comboIndex != _combo.Count)
            {
                int i = (int)_buttonPresses.Peek();
                if (i == _combo[_comboIndex])
                {
                    // correct press
                    _buttons[i].Correct();
                    Indicators.Increment();
                    _comboIndex++;
                    _correctFlashTimer = _correctFlashTime;
                } else
                {
                    // incorrect press
                    _comboIndex = 0;
                    // todo: should player take damage?
                    Indicators.Incorrect();
                    _buttons[i].Incorrect();
                    _buttons[_combo[_comboIndex]].Disable();
                    _resetTimer = _resetTime;
                }
            }

            if (_comboIndex == _combo.Count)
            {
                // combo completed
                GenerateCombo();
                _resetTimer = _resetTime;
                _enemy.TakeDamage(10);
                break;
            }

            _buttonPresses.Dequeue();
        }
        _buttonPresses.Clear();
    }

    void GenerateCombo()
    {
        _combo.Clear();
        for (int i = 0; i < _comboSize; i++)
        {
            _combo.Add(Random.Range(0, 3));
        }
        _comboIndex = 0;
    }

    public void ChangeEnabled(bool enabled)
    {
        _enabled = enabled;
        _resetTimer = 0;
        _correctFlashTimer = 0;
        _comboIndex = 0;

        foreach (SpriteRenderer renderer in _childRenderers)
        {
            renderer.enabled = enabled;
        }

        if (_enabled)
        {
            Indicators.Clear();
            foreach (Button button in _buttons)
            {
                button.Disable();
            }
            _buttons[_combo[_comboIndex]].Enable();
        }
    }

    #region Input
    void OnTopButton()
    {
        if (_enabled)
        {
            _buttonPresses.Enqueue(ButtonIndex.Top);
        }
    }

    void OnBottomButton()
    {
        if (_enabled)
        {
            _buttonPresses.Enqueue(ButtonIndex.Bottom);
        }
    }

    void OnLeftButton()
    {
        if (_enabled)
        {
            _buttonPresses.Enqueue(ButtonIndex.Left);
        }
    }

    void OnRightButton()
    {
        if (_enabled)
        {
            _buttonPresses.Enqueue(ButtonIndex.Right);
        }
    }
    #endregion
}
