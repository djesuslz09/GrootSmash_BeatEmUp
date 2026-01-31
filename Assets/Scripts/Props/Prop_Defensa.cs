using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProp : MonoBehaviour
{
    public int extraHealth = 10; // lo que equivale a "un golpe más"

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.IncreaseMaxHealth(extraHealth);
            }

            Destroy(gameObject);
        }
    }
}