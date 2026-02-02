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

    [Header("--- Control ---")]
    private bool yaSeEnfadoPorProximidad = false;
    private int enemiesKilled = 0;
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Busca al jugador
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            jugador = playerObj.transform;

        // Empezamos en idle
        if (animator != null && !string.IsNullOrEmpty(idleStateName))
        {
            animator.Play(idleStateName);
        }
    }

    void Update()
    {
        if (jugador == null || yaSeEnfadoPorProximidad) return;

        // Detecta si el jugador está cerca
        float distancia = Vector2.Distance(transform.position, jugador.position);
        
        if (distancia <= rangoDeteccion)
        {
            ActivarEnfadoPorProximidad();
        }
    }

    private void ActivarEnfadoPorProximidad()
    {
        yaSeEnfadoPorProximidad = true;
        MirarJugador();
        
        // Reproduce animación de enfado
        if (animator != null && !string.IsNullOrEmpty(angryStateName))
        {
            animator.Play(angryStateName);
        }
        
        Debug.Log("¡Boss enfadado por proximidad del jugador!");
    }

    // Llamar desde GameManager2 cuando muera un enemigo
    public void OnEnemyKilled()
    {
        enemiesKilled++;
        MirarJugador();

        // Reproduce animación de enfado
        if (animator != null && !string.IsNullOrEmpty(angryStateName))
        {
            animator.Play(angryStateName);
        }

        Debug.Log("Boss enfadado. Enemigos muertos: " + enemiesKilled);
    }

    private void MirarJugador()
    {
        if (jugador == null) return;
        
        if (transform.position.x < jugador.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    // Gizmos para ver el rango
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}


/*using UnityEngine;

public class BossAngryController : MonoBehaviour
{
    [Header("Animaciones")]
    public Animator animator;                    // Animator del boss
    public string idleStateName = "BossIdle";   // nombre del clip idle
    public string angryStateName = "BossAngry"; // nombre del clip enfado

    [Header("Enemigos")]
    public int totalEnemies = 5;                // cu�ntos enemigos hay
    private int enemiesKilled = 0;

    void Start()
    {
        // Empezamos en idle
        if (animator != null && !string.IsNullOrEmpty(idleStateName))
        {
            animator.Play(idleStateName);
        }
    }

    // Llamar desde los enemigos cuando mueran
    public void OnEnemyKilled()
    {
        enemiesKilled++;

        // Reproducir animaci�n de enfado
        if (animator != null && !string.IsNullOrEmpty(angryStateName))
        {
            animator.Play(angryStateName);
        }

        // Si se murieron todos
        if (enemiesKilled >= totalEnemies)
        {
            Debug.Log("�Todos los enemigos muertos! Fase del boss final.");
            // Aqu� puedes: activar ataques del boss, cambiar m�sica, etc.
            // animator.Play("BossAttack");
        }
    }
}*/