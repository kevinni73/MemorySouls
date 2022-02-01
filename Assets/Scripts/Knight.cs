using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    // replace swords on death
    [SerializeField] GameObject DummySwordPrefab;

    [SerializeField] float _attackCooldownTime = 2f;
    float _attackCooldownTimer;

    Enemy EnemyComponent;
    List<Sword> Swords;
    int _nextSwordIndex = 0;

    bool _secondPhase;

    void Awake()
    {
        Swords = new List<Sword>(GetComponentsInChildren<Sword>());
        EnemyComponent = GetComponent<Enemy>();

        EnemyComponent.onTakeDamage += onTakeDamage;
    }

    void Update()
    {
        if (_attackCooldownTimer > 0)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }

        if (Swords.Count > 0 && Swords[_nextSwordIndex].AttackReady && _attackCooldownTimer <= 0)
        {
            Swords[_nextSwordIndex].Attack();
            _nextSwordIndex = (_nextSwordIndex + 1) % Swords.Count;
            _attackCooldownTimer = _attackCooldownTime;
        }
    }

    void onTakeDamage()
    {
        if (!_secondPhase && EnemyComponent.Health <= EnemyComponent.MaxHealth / 2)
        {
            _secondPhase = true;
            GetComponentInChildren<AttackButtons>().IncreaseComboSize();
        }
        else if (EnemyComponent.Health <= 0)
        {
            foreach (Sword sword in Swords)
            {
                var dummySword = Instantiate(DummySwordPrefab, sword.transform.position, sword.transform.rotation);
                Destroy(sword.gameObject);
            }
            Swords.Clear();
        }
    }
}
