using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseProp : MonoBehaviour
{
    public int defense = 20; // Cantidad de defensa extra

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent< PlayerController > ();
            if (player != null)
            {
                player.IncreaseDefense(defense);
            }

            Destroy(gameObject);
        }
    }
}