using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProp : MonoBehaviour
{
    public int extraDefense = 5; // Cantidad de defensa extra

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prueba player = other.GetComponent<prueba>();
            if (player != null)
            {
                player.IncreaseDefense(extraDefense);
            }

            Destroy(gameObject);
        }
    }
}