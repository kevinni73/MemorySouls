using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] float _attackCooldownTime = 2f;
    float _attackCooldownTimer;

    Sword[] Swords;
    int _nextSwordIndex = 0;

    Enemy EnemyComponent;

    void Awake()
    {
        Swords = GetComponentsInChildren<Sword>();
        EnemyComponent = GetComponent<Enemy>();
    }

    void Update()
    {
        if (EnemyComponent.Health <= 0)
        {
            return;
        }

        if (_attackCooldownTimer > 0)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }

        if (Swords.Length > 0 && Swords[_nextSwordIndex].AttackReady && _attackCooldownTimer <= 0)
        {
            Swords[_nextSwordIndex].Attack();
            _nextSwordIndex = (_nextSwordIndex + 1) % Swords.Length;
            _attackCooldownTimer = _attackCooldownTime;
        }
    }
}
