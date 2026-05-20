using System.Collections;
using UnityEngine;

public class BossAngryController : MonoBehaviour
{
    [Header("--- Animaciones ---")]
    public Animator animator;
    public string idleStateName = "BossIdle";
    public string angryStateName = "BossAngry";

    [Header("--- Detección Jugador ---")]
    private Transform jugador;
    [SerializeField] private float rangoDeteccion = 5f;

    [Header("--- Proyectiles ---")]
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadProyectil = 5f;
    [SerializeField] private float tiempoEntreDisparos = 3f;

    [Header("--- Control ---")]
    private bool yaSeEnfadoPorProximidad = false;
    private int enemiesKilled = 0;
    private float cronometroDisparo = 0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            jugador = playerObj.transform;
        else
            Debug.LogWarning("¡No se encontró jugador con tag 'Player'!");

        if (animator != null && !string.IsNullOrEmpty(idleStateName))
            animator.Play(idleStateName);

        cronometroDisparo = Time.time + tiempoEntreDisparos;
    }

    void Update()
    {
        if (jugador == null) return;

        MirarJugador();

        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Activar enfado una sola vez al detectar al jugador
        if (!yaSeEnfadoPorProximidad && distancia <= rangoDeteccion)
        {
            ActivarEnfadoPorProximidad();
        }

        // Disparar si ya se enfadó y el jugador está en rango
        if (yaSeEnfadoPorProximidad && distancia <= rangoDeteccion)
        {
            if (Time.time >= cronometroDisparo)
            {
                LanzarProyectil();
                cronometroDisparo = Time.time + tiempoEntreDisparos;
            }
        }
    }

    private void ActivarEnfadoPorProximidad()
    {
        yaSeEnfadoPorProximidad = true;

        if (animator != null && !string.IsNullOrEmpty(angryStateName))
            animator.Play(angryStateName);

        Debug.Log("¡Boss enfadado por proximidad del jugador!");
    }

    private void LanzarProyectil()
    {
        if (proyectilPrefab == null || puntoDisparo == null || jugador == null) return;

        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        Vector2 direccion = (jugador.position - puntoDisparo.position).normalized;

        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direccion * velocidadProyectil;

        Debug.Log("¡Kingpin dispara!");
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;

        if (animator != null && !string.IsNullOrEmpty(angryStateName))
            animator.Play(angryStateName);

        Debug.Log("Boss enfadado. Enemigos muertos: " + enemiesKilled);
    }

    private void MirarJugador()
    {
        if (jugador == null || spriteRenderer == null) return;
        spriteRenderer.flipX = transform.position.x < jugador.position.x;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}