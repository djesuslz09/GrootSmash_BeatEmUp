using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProp : MonoBehaviour
{
    public int extraHealth = 20; // Cantidad de vida que da

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Acceder al script del jugador
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // Sumamos vida (si quieres un sistema de vida, agrega una variable 'health' en 'prueba')
                player.IncreaseHealth(extraHealth);
            }

            Destroy(gameObject); // El prop desaparece
        }
    }
}