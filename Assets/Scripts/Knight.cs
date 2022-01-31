using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] float _attackCooldownTime = 2f;
    float _attackCooldownTimer;

    [SerializeField] GameObject DummySwordPrefab;
    Sword[] Swords;
    int _nextSwordIndex = 0;

    Enemy EnemyComponent;

    bool _stopped = false;

    void Awake()
    {
        Swords = GetComponentsInChildren<Sword>();
        EnemyComponent = GetComponent<Enemy>();
    }

    void Update()
    {
        if (EnemyComponent.Health <= 0)
        {
            if (!_stopped)
            {
                _stopped = true;
                foreach (Sword sword in Swords)
                {
                    var dummySword = Instantiate(DummySwordPrefab, sword.transform.position, sword.transform.rotation);
                    Destroy(sword.gameObject);
                }
            }
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
