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
            PlayerController_Props_Prueba player = other.GetComponent<PlayerController_Props_Prueba>();
            if (player != null)
            {
                // Sumamos vida (si quieres un sistema de vida, agrega una variable 'health' en 'prueba')
                player.Curar(extraHealth);
            }

            Destroy(gameObject); // El prop desaparece
        }
    }
}

