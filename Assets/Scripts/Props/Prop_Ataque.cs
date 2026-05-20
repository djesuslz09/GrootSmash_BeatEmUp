using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthProp : MonoBehaviour
{
    public int extraDamage = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Tag estándar "Player"
        if (other.CompareTag("Player"))
        {
            //pruebas unificado
            PlayerController_Props_Prueba player = other.GetComponent<PlayerController_Props_Prueba>();

            if (player != null)
            {
                // subir el daño
                player.IncreaseDamage(extraDamage);

                // El prop desaparece 
                Destroy(gameObject);
            }
        }
    }
}