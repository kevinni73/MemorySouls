using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    AttackButtons AttackButtons;

    private void Awake()
    {
        AttackButtons = FindObjectOfType<AttackButtons>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AttackButtons.ChangeEnabled(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AttackButtons.ChangeEnabled(false);
        }
    }
}
