using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerTrigger : MonoBehaviour
{
    [SerializeField] int _damage = 10;

    void CheckPlayerDamage(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(_damage, collision.transform.position - this.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayerDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckPlayerDamage(collision);
    }
}
