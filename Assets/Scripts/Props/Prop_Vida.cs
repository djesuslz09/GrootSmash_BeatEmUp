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
            // Corregido: Buscamos el nombre EXACTO de tu script del jugador
            PlayerController_Props_Prueba player = other.GetComponent<PlayerController_Props_Prueba>();

            if (player != null)
            {
                // Corregido: Llamamos a la función "Curar" que añadimos antes
                player.Curar(extraHealth);

                // El prop desaparece solo si el jugador lo ha tocado con éxito
                Destroy(gameObject);
            }
        }
    }
}