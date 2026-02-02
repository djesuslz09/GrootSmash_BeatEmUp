using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("--- Estadísticas ---")]
    [SerializeField] private float vida = 100f;
    
    [Header("--- IA Combate ---")]
    private Transform jugador;       // Arrastra aquí a tu Player
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float rangoDeteccion = 5f; // Distancia para empezar a seguirte
    [SerializeField] private float rangoAtaque = 1f;    // Distancia para pegar
    [SerializeField] private float damageAtaque = 10f;
    [SerializeField] private float tiempoEntreAtaques = 2f; // Cooldown

    private float cronometroAtaque;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Si no asignaste el jugador manualmente, intenta buscarlo por etiqueta
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) jugador = playerObj.transform;
        }
    }

    private void Update()
    {
        if (estaMuerto || jugador == null) return;

        ComportamientoIA();
    }

    private void ComportamientoIA()
    {
        // Calcular distancia con el jugador
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia < rangoDeteccion)
        {
            // Girar sprite para mirar al jugador
            MirarJugador();

            if (distancia > rangoAtaque)
            {
                // PERSEGUIR: Si lo veo pero está lejos, me muevo hacia él
                transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
                //animator.SetBool("Caminando", true); // Asegúrate de tener este Bool en tu Animator
            }
            else
            {
                // ATACAR: Estoy en rango de golpe
                //animator.SetBool("Caminando", false);
                
                if (Time.time >= cronometroAtaque)
                {
                    Atacar();
                    cronometroAtaque = Time.time + tiempoEntreAtaques;
                }
            }
        }
        else
        {
            // IDLE: Jugador lejos
            //animator.SetBool("Caminando", false);
        }
    }

    private void MirarJugador()
    {
        // Si el jugador está a la derecha (x mayor), flipX false. Si está a la izquierda, true.
        if (transform.position.x < jugador.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    private void Atacar()
    {
        animator.SetTrigger("Atacar"); // Crea este Trigger en tu Animator

        // Lógica simple de daño: Asumimos que si ataca, golpea (puedes mejorar esto con Hitbox luego)
        // Buscamos si el jugador tiene script para recibir daño
        PlayerController playerScript = jugador.GetComponent<PlayerController>();
        if (playerScript != null)
        {
            playerScript.RecibirDaño(damageAtaque);
        }
    }

    // --- Parte de Recibir Daño (Mantenida de tu código anterior) ---
    public void TomarDamage(float damage)
    {
        if (estaMuerto) return;

        vida -= damage;
        //animator.SetTrigger("Hurt"); // Opcional: animación de herido

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        estaMuerto = true;
        animator.SetTrigger("Muerte");
        GetComponent<Collider2D>().enabled = false; // Desactiva colisiones para no bloquear

        // Avisa al GameManager2 de que este enemigo ha muerto
        if (GameManager2.Instance != null)
            GameManager2.Instance.NotifyEnemyDeath();
            this.enabled = false; // Desactiva este script
            // Inicia el fade a rojo
    StartCoroutine(OscurecerYDestruir());
}

private IEnumerator OscurecerYDestruir()
{
    float duracion = 0.4f;
    float tiempo = 0f;
    Color colorInicial = spriteRenderer.color;
    Color colorFinal = Color.red;
    
    while (tiempo < duracion)
    {
        tiempo += Time.deltaTime;
        spriteRenderer.color = Color.Lerp(colorInicial, colorFinal, tiempo / duracion);
        yield return null;
    }
    
    Destroy(gameObject,0.2f);
}
    // Dibujar los rangos en el editor para verlos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}