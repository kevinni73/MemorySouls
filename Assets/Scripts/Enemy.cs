using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        _animator.SetTrigger("Hurt");

        Debug.Log("Enemy health: " + _health);
    }
}
