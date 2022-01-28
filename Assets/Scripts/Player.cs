using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables

    // Movement
    [SerializeField] float _runSpeed = 8;

    // Roll
    bool _rolled = false;
    [SerializeField] float _rollSpeed = 16;
    [SerializeField] float _rollTime = 0.2f;
    [SerializeField] float _rollCooldownTime = 0.25f;
    float _rollCooldownTimer = 0;
    Vector2 _rollDir = Vector2.up;

    // Hit knockback
    const float _knockbackSpeed = 10;
    const float _knockbackTime = 0.1f;
    const float _endKnockbackMult = 0.25f;
    Vector2 _knockbackDir;

    // General Movement
    Vector2 _velocity;
    Vector2 _moveInput;
    Vector2 _lastDir = Vector2.up;
    float _lastDirectionFloat = 0.25f; // from 0-1, going clockwise from Vector.right

    // Invincibility
    const float _invincibilityDuration = 1f;
    const float _respawnInvincibilityDuration = 0.5f;
    const float _invincibilityFlashTime = 0.1f;
    bool _invincible = false;

    // Health
    public HealthBar PlayerHealthBar;
    int _health = 100;

    // Other components
    SpriteRenderer _renderer;
    StateMachine _stateMachine;
    BoxCollider2D _collider;
    Rigidbody2D _rb;
    Animator _animator;

    #endregion


    #region Internal types
    public enum State
    {
        Normal,
        Roll,
        Damaged,
    };
    #endregion


    #region Monobehavior
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _stateMachine = gameObject.AddComponent<StateMachine>();
        _stateMachine.Init(Enum.GetNames(typeof(State)).Length);
        _stateMachine.AddState((int)State.Normal, NormalUpdate, null, null, null);
        _stateMachine.AddState((int)State.Roll, null, RollCoroutine, RollBegin, RollEnd);
        _stateMachine.AddState((int)State.Damaged, null, DamagedCoroutine, DamagedBegin, DamagedEnd);
    }

    void Update()
    {
        if (_rollCooldownTimer > 0)
        {
            _rollCooldownTimer -= Time.deltaTime;
        }

        _animator.SetFloat("SpeedX", _velocity.x);
        _animator.SetFloat("SpeedY", _velocity.y);
        _animator.SetFloat("Speed", _velocity.sqrMagnitude);
        _animator.SetFloat("LastDir", _lastDirectionFloat);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
    }
    #endregion


    #region Normal State

    int NormalUpdate()
    {
        if (_rolled)
        {
            _rolled = false;
            return (int)State.Roll;
        }

        _velocity = _moveInput.normalized * _runSpeed;

        return (int)State.Normal;
    }
    #endregion


    #region Roll State

    void RollBegin()
    {
        _animator.SetFloat("RollDir", DirectionToAngleRange01(_rollDir));
        _animator.SetBool("Rolling", true);
        _velocity = _rollDir.normalized * _rollSpeed;
    }

    void RollEnd()
    {
        _animator.SetBool("Rolling", false);
        _velocity = Vector2.zero;
        _rollCooldownTimer = _rollCooldownTime;
    }

    IEnumerator RollCoroutine()
    {
        yield return new WaitForSeconds(_rollTime);
        _stateMachine.State = (int)State.Normal;
    }

    #endregion


    #region Damaged State

    void DamagedBegin()
    {
        _velocity = _knockbackDir.normalized * _knockbackSpeed;
        BecomeInvincibile(_invincibilityDuration);
    }

    void DamagedEnd()
    {
        _velocity *= _endKnockbackMult;
    }

    IEnumerator DamagedCoroutine()
    {
        yield return new WaitForSeconds(_knockbackTime);
        _stateMachine.State = (int)State.Normal;
    }

    #endregion


    #region Inputs

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();

        if (_moveInput != Vector2.zero)
        {
            _lastDir = _moveInput.normalized;
            _lastDirectionFloat = DirectionToAngleRange01(_moveInput);
        }

        //Debug.Log(_moveInput);
    }

    void OnRoll(InputValue value)
    {
        if (_rollCooldownTimer <= 0 && _stateMachine.State != (int)State.Roll)
        {
            _rollDir = _lastDir;
            _rolled = true;
        }
    }

    #endregion


    #region Public Player API

    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (_invincible)
        {
            return;
        }

        if (_stateMachine.State != (int)State.Damaged)
        {
            _health -= damage;
            PlayerHealthBar.SetHealth(_health);
            _knockbackDir = knockback;
            _stateMachine.State = (int)State.Damaged;
        }
    }

    #endregion


    #region Private Player Methods

    // Gets the angle of a vector and transform it to a range of [0, 1),
    // starting from Vector.right and going counterclockwise
    float DirectionToAngleRange01(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) / (Mathf.PI * 2);
        if (angle < 0)
        {
            angle += 1;
        }
        return angle;
    }

    void BecomeInvincibile(float invicibilityDuration)
    {
        IEnumerator coroutine = InvincibilityCoroutine(invicibilityDuration);
        StartCoroutine(coroutine);
    }

    IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        _invincible = true;
        bool flash = true;

        for (float timer = 0; timer < invincibilityDuration; timer += _invincibilityFlashTime)
        {
            _renderer.color = flash ? Color.red : Color.white;
            flash = !flash;
            yield return new WaitForSeconds(_invincibilityFlashTime);
        }

        _renderer.color = Color.white;
        _invincible = false;
    }
    #endregion
}
