using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [Header("Ataque J")]
    [SerializeField] private Transform controladorGolpeJ;
    [SerializeField] private float radioGolpeJ;
    [SerializeField] private float damageGolpeJ;

    [Header("Ataque K")]
    [SerializeField] private Transform controladorGolpeK;
    [SerializeField] private float radioGolpeK;
    [SerializeField] private float damageGolpeK;

    [Header("Ataque L")]
    [SerializeField] private Transform controladorGolpeL;
    [SerializeField] private float radioGolpeL;
    [SerializeField] private float damageGolpeL;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            Golpe(controladorGolpeJ, radioGolpeJ, damageGolpeJ);

        if (Input.GetKeyDown(KeyCode.K))
            Golpe(controladorGolpeK, radioGolpeK, damageGolpeK);

        if (Input.GetKeyDown(KeyCode.L))
            Golpe(controladorGolpeL, radioGolpeL, damageGolpeL);
    }

    private void Golpe(Transform puntoGolpe, float radio, float damage)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(puntoGolpe.position, radio);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.GetComponent<Enemigo>().TomarDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (controladorGolpeJ != null)
            Gizmos.DrawWireSphere(controladorGolpeJ.position, radioGolpeJ);

        if (controladorGolpeK != null)
            Gizmos.DrawWireSphere(controladorGolpeK.position, radioGolpeK);

        if (controladorGolpeL != null)
            Gizmos.DrawWireSphere(controladorGolpeL.position, radioGolpeL);
    }
}
