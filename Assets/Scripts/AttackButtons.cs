using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtons : MonoBehaviour
{
    private enum ButtonIndex
    {
        Top,
        Bottom,
        Left,
        Right,
    }
    Queue<ButtonIndex> _buttonPresses = new Queue<ButtonIndex>();

    [SerializeField] AttackButton TopButton;
    [SerializeField] AttackButton BottomButton;
    [SerializeField] AttackButton LeftButton;
    [SerializeField] AttackButton RightButton;
    List<AttackButton> _buttons;

    [SerializeField] Enemy _enemy;

    [SerializeField] float _resetTime = 0.05f;
    float _resetTimer;

    [SerializeField] float _correctFlashTime = 0.05f;
    float _correctFlashTimer;

    int _comboSize = 3;
    List<int> _combo = new List<int>();
    int _comboIndex = 0;

    IndicatorManager Indicators;
    List<SpriteRenderer> _childRenderers;

    bool _enabled;

    InputManager Input;

    void Awake()
    {
        Indicators = GetComponentInChildren<IndicatorManager>();
        Indicators.Init(_comboSize);

        // For convenient accessing buttons by index, make sure it matches ButtonIndex order
        _buttons = new List<AttackButton> { TopButton, BottomButton, LeftButton, RightButton };

        Input = FindObjectOfType<InputManager>();
        Input.onTopButtonEvent += OnTopButton;
        Input.onBottomButtonEvent += OnBottomButton;
        Input.onLeftButtonEvent += OnLeftButton;
        Input.onRightButtonEvent += OnRightButton;
    }

    void OnDestroy()
    {
        Input.onTopButtonEvent -= OnTopButton;
        Input.onBottomButtonEvent -= OnBottomButton;
        Input.onLeftButtonEvent -= OnLeftButton;
        Input.onRightButtonEvent -= OnRightButton;
    }

    void Start()
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
                ChangeEnabled(true);
            }

            return;
        }

        if (_correctFlashTimer > 0)
        {
            _correctFlashTimer -= Time.deltaTime;
            if (_correctFlashTimer <= 0)
            {
                _buttons[_combo[_comboIndex - 1]].Deselect();
                _buttons[_combo[_comboIndex]].Select();
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
                }
                else
                {
                    // incorrect press
                    _comboIndex = 0;
                    Indicators.Incorrect();
                    _buttons[i].Incorrect();
                    _buttons[_combo[_comboIndex]].Deselect();
                    _resetTimer = _resetTime;
                }
            }

            if (_comboIndex == _combo.Count)
            {
                // combo completed
                GenerateCombo();
                _resetTimer = _resetTime;
                _enemy.TakeDamage(20);
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
            _combo.Add(Random.Range(0, _comboSize));
        }
        _comboIndex = 0;
    }

    void ChangeEnabled(bool enabled)
    {
        _enabled = enabled;
        _resetTimer = 0;
        _correctFlashTimer = 0;
        _comboIndex = 0;

        if (_enabled)
        {
            Indicators.Clear();
            Indicators.Enable();
            foreach (AttackButton button in _buttons)
            {
                button.Deselect();
                button.Enable();
            }

            _buttons[_combo[_comboIndex]].Select();
        }
        else
        {
            Indicators.Disable();
            foreach (AttackButton button in _buttons)
            {
                button.Disable();
            }
        }
    }

    public void IncreaseComboSize()
    {
        _comboSize++;
        _combo.Add(Random.Range(0, _comboSize));
        Indicators.Init(_comboSize);
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

    #region Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeEnabled(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeEnabled(false);
        }
    }

    #endregion
}
