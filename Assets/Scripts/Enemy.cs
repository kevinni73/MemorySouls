using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action onEnemyDeadEvent;

    public HealthBar EnemyHealthBar;
    [SerializeField] int _health = 100;

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

            if (_animator != null)
            {
                _animator.SetTrigger("Dead");
            }
        }
        else
        {
            if (_animator != null)
            {
                _animator.SetTrigger("Hurt");
            }
        }
    }
}
