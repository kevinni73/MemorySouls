using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private enum State
    {
        Waiting,
        Targeting,
        Moving,
    };

    [SerializeField] float _rotationSpeed = 6f;
    [SerializeField] float _moveSpeed = 100f;

    [SerializeField] float _waitTime = 1f;
    [SerializeField] float _flashTime = 0.1f;

    float travelDistance;

    public Vector2 _velocity;

    StateMachine _stateMachine;
    Rigidbody2D _rb;
    SpriteRenderer _renderer;

    [SerializeField] Color _flashColor;

    GameObject Player;

    public bool AttackReady = false;
    bool _attack = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        Player = FindObjectOfType<Player>().gameObject;

        _stateMachine = gameObject.AddComponent<StateMachine>();
        _stateMachine.Init(Enum.GetNames(typeof(State)).Length);
        _stateMachine.AddState((int)State.Waiting, null, WaitingCoroutine, null, null);
        _stateMachine.AddState((int)State.Targeting, TargetingUpdate, null, null, null);
        _stateMachine.AddState((int)State.Moving, MovingUpdate, null, MovingBegin, MovingEnd);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = _velocity * Time.fixedDeltaTime;
        if (travelDistance > 0)
        {
            travelDistance -= velocity.magnitude;
        }
        _rb.MovePosition(_rb.position + velocity);
    }

    IEnumerator WaitingCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        _stateMachine.State = (int)State.Targeting;
    }

    int TargetingUpdate()
    {
        if (_attack)
        {
            _attack = false;
            AttackReady = false;
            StartCoroutine(FlashCoroutine());
        }

        Vector2 vectorToTarget = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 270;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Quaternion.Angle(transform.rotation, q) < 5)
        {
            AttackReady = true;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);

        return (int)State.Targeting;
    }

    IEnumerator FlashCoroutine()
    {
        _renderer.color = _flashColor;
        yield return new WaitForSeconds(_flashTime);
        _renderer.color = Color.white;
        _stateMachine.State = (int)State.Moving;
    }

    void MovingBegin()
    {
        Vector2 vectorToTarget = Player.transform.position - transform.position;
        travelDistance = vectorToTarget.magnitude;
        _velocity = vectorToTarget.normalized * _moveSpeed;
    }

    void MovingEnd()
    {
        _velocity = Vector2.zero;
    }

    int MovingUpdate()
    {
        if (travelDistance <= 0)
        {
            return (int)State.Waiting;
        }

        return (int)State.Moving;
    }

    public void Attack()
    {
        _attack = true;
    }
}
