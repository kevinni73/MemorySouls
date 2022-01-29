using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    int _health = 100;

    [SerializeField] float _attackCooldownTime = 2f;
    float _attackCooldownTimer;

    Sword[] Swords;
    Animator _animator;
    int _nextSwordIndex = 0;

    void Awake()
    {
        Swords = GetComponentsInChildren<Sword>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_health <= 0)
        {
            return;
        }

        if (_attackCooldownTimer > 0)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }

        if (Swords[_nextSwordIndex].AttackReady && _attackCooldownTimer <= 0)
        {
            Swords[_nextSwordIndex].Attack();
            _nextSwordIndex = (_nextSwordIndex + 1) % Swords.Length;
            _attackCooldownTimer = _attackCooldownTime;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hurt");
    }
}
