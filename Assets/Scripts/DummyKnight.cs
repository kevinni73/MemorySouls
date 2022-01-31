using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyKnight : MonoBehaviour
{
    Enemy _enemyComponent;
    [SerializeField] List<GameObject> ObjectsToEnable;
    [SerializeField] List<GameObject> ObjectsToDestroy;

    [SerializeField] GameObject _replacementPrefab;

    void Awake()
    {
        _enemyComponent = GetComponent<Enemy>();
        _enemyComponent.onEnemyDeadEvent += Dead;
    }

    void Dead()
    {
        foreach (GameObject gameObject in ObjectsToEnable) {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in ObjectsToDestroy)
        {
            Destroy(gameObject);
        }

        GameObject knight = Instantiate(_replacementPrefab, transform.position, Quaternion.identity);
        knight.GetComponent<Enemy>().EnemyHealthBar = GameObject.Find("Boss Health Bar").GetComponent<HealthBar>();

        knight.GetComponent<Animator>().SetTrigger("Hurt");

        Destroy(this.gameObject);
    }
}
