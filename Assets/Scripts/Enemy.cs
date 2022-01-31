using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action onEnemyDeadEvent;

    [SerializeField] HealthBar EnemyHealthBar;
    int _health = 100;

    public int Health
    {
        get => _health;
    }

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (EnemyHealthBar)
        {
            EnemyHealthBar.SetHealth(_health);
        }

        if (_health <= 0)
        {
            if (onEnemyDeadEvent != null)
            {
                onEnemyDeadEvent();
            }

            _animator.SetTrigger("Dead");
        }
        else
        {
            _animator.SetTrigger("Hurt");
        }
    }
}
