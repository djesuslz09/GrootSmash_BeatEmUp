using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProp : MonoBehaviour
{
    public int healthAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si es el jugador
        if (other.CompareTag("Player"))
        {
            // Intentamos obtener el script de vida del jugador
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.AddHealth(healthAmount);
            }

            // Destruimos el prop tras recogerlo
            Destroy(gameObject);
        }
    }
}