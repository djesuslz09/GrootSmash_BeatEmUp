using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para usar el componente Image


public class ControladorCorazones : MonoBehaviour
{
    [SerializeField] private PlayerController player; // Arrastra al Player aquí
    [SerializeField] private Image[] corazones;       // Arrastra las 5 imágenes de corazones aquí
    [SerializeField] private Sprite corazonLleno;
    [SerializeField] private Sprite corazonVacio;

    void Update()
    {
        ActualizarCorazones();
    }

    void ActualizarCorazones()
    {
        float vidaActual = player.ObtenerVida();

        for (int i = 0; i < corazones.Length; i++)
        {
            // Cada corazón representa 20 puntos. 
            // Ejemplo: Si vida es 40, los corazones 0 y 1 (20 y 40) estarán llenos.
            if (vidaActual >= (i + 1) * 20)
            {
                corazones[i].sprite = corazonLleno;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }
        }
    }
}