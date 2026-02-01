using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthProp : MonoBehaviour
{
    public int extraDamage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();

            if (playerAttack != null)
            {
               playerAttack.IncreaseDamage(extraDamage);
            }

            Destroy(gameObject);
        }
    }
}