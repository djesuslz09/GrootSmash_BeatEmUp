using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemaCombate : MonoBehaviour
{ 
    //Llamar a la función para los props (ataque)
    public int damage = 10;

    public void IncreaseDamage(int amount)
    {
        damage += amount;
        Debug.Log("Daño aumentado: " + damage);
    }




    [Header("--- Componentes ---")]

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("--- Combate General ---")]
    [SerializeField] private LayerMask capaEnemigos; // Importante para optimizar colisiones

    [Header("--- Configuración Ataque J ---")]
    [SerializeField] private Transform puntoGolpeJ;
    [SerializeField] private float radioGolpeJ = 0.5f;
    [SerializeField] private float damageGolpeJ = 10f;

    [Header("--- Configuración Ataque K ---")]
    [SerializeField] private Transform puntoGolpeK;
    [SerializeField] private float radioGolpeK = 0.5f;
    [SerializeField] private float damageGolpeK = 20f;

    [Header("--- Configuración Ataque L ---")]
    [SerializeField] private Transform puntoGolpeL;
    [SerializeField] private float radioGolpeL = 0.5f;
    [SerializeField] private float damageGolpeL = 30f;

    //[Header("--- Estadísticas Jugador ---")]
    //[SerializeField] private float vidaJugador = 100f;
    //private float vidaMaxima = 100f;
    private void Start()
    {
        // Obtenemos los componentes automáticamente si están en el mismo objeto
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ProcesarCombate();
    }
    private void ProcesarCombate()
    {
        // Ataque J
        if (Input.GetKeyDown(KeyCode.J))
        {
            EjecutarAtaque("Attack", puntoGolpeJ, radioGolpeJ, damageGolpeJ);
        }

        // Ataque K (Asegúrate de tener triggers en el Animator o usa el mismo "Attack")
        if (Input.GetKeyDown(KeyCode.K))
        {
            EjecutarAtaque("Attack_2", puntoGolpeK, radioGolpeK, damageGolpeK);
        }

        // Ataque L
        if (Input.GetKeyDown(KeyCode.L))
        {
            EjecutarAtaque("Attack_3", puntoGolpeL, radioGolpeL, damageGolpeL);
        }
    }

    private void EjecutarAtaque(string triggerAnimacion, Transform puntoGolpe, float radio, float damage)
    {
        // 1. Animación
        // Reseteamos el trigger por si el jugador spamea el botón
        animator.ResetTrigger(triggerAnimacion); 
        animator.SetTrigger(triggerAnimacion);

        // 2. Detección de daño
        // Nota: Idealmente el daño se hace en un "Animation Event", pero para mantener
        // tu lógica simple, lo haremos aquí al pulsar la tecla.
        if (puntoGolpe != null)
        {
            Collider2D[] objetos = Physics2D.OverlapCircleAll(puntoGolpe.position, radio, capaEnemigos);

            // Línea temporal de debug
            Debug.Log("Ataque: " + triggerAnimacion + " | Objetos detectados: " + objetos.Length + " | Punto: " + puntoGolpe.position + " | Radio: " + radio);

            foreach (Collider2D colisionador in objetos)
            {
                // Verificamos si tiene el script Enemigo antes de llamar
                Enemigo enemigoScript = colisionador.GetComponent<Enemigo>();
                if (enemigoScript != null)
                {
                    enemigoScript.TomarDamage(damage);
                }
            }
        }
    }

    // Para ver los radios de ataque en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (puntoGolpeJ != null) Gizmos.DrawWireSphere(puntoGolpeJ.position, radioGolpeJ);
        
        Gizmos.color = Color.yellow;
        if (puntoGolpeK != null) Gizmos.DrawWireSphere(puntoGolpeK.position, radioGolpeK);
        
        Gizmos.color = Color.blue;
        if (puntoGolpeL != null) Gizmos.DrawWireSphere(puntoGolpeL.position, radioGolpeL);
    }



    // Función para que la UI sepa cuánta vida queda
    //public float ObtenerVida() => vidaJugador;
}


