using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action onTakeDamage;

    public HealthBar EnemyHealthBar;
    [SerializeField] int _health = 100;

    public int Health
    {
        get => _health;
    }

    public int MaxHealth;

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        MaxHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (EnemyHealthBar)
        {
            EnemyHealthBar.SetHealth(_health);
        }

        if (onTakeDamage != null)
        {
            onTakeDamage();
        }

        if (_animator != null)
        {
            if (_health <= 0)
            {
                _animator.SetTrigger("Dead");
            } else
            {
                _animator.SetTrigger("Hurt");
            }
        }
    }
}
