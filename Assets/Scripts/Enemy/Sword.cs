using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region Variables

    [SerializeField] float _rotationSpeed = 6f;
    [SerializeField] float _moveSpeed = 100f;

    [SerializeField] float _waitTime = 1f;
    [SerializeField] float _flashTime = 0.1f;
    [SerializeField] Color _flashColor;

    StateMachine _stateMachine;
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    GameObject Player;

    Vector2 _velocity;
    float _travelDistance;
    public bool AttackReady = false;
    bool _attack = false;
    #endregion

    #region Internal Types
    private enum State
    {
        Waiting,
        Targeting,
        Moving,
    };
    #endregion

    #region Monobehavior
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

    void FixedUpdate()
    {
        Vector2 velocity = _velocity * Time.fixedDeltaTime;
        if (_travelDistance > 0)
        {
            _travelDistance -= velocity.magnitude;
        }
        _rb.MovePosition(_rb.position + velocity);
    }
    #endregion

    #region Waiting State
    IEnumerator WaitingCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        _stateMachine.State = (int)State.Targeting;
    }
    #endregion

    #region Targeting State
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
    #endregion

    #region Moving State
    void MovingBegin()
    {
        Vector2 vectorToTarget = Player.transform.position - transform.position;
        _travelDistance = vectorToTarget.magnitude;
        _velocity = vectorToTarget.normalized * _moveSpeed;
    }

    void MovingEnd()
    {
        _velocity = Vector2.zero;
    }

    int MovingUpdate()
    {
        if (_travelDistance <= 0)
        {
            return (int)State.Waiting;
        }

        return (int)State.Moving;
    }
    #endregion

    #region Public Methods
    public void Attack()
    {
        _attack = true;
    }
    #endregion
}
