using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("--- Detección Jugador ---")]
    private Transform jugador;
    [SerializeField] private float rangoDeteccion = 5f; // Distancia para activar enfado
    
    [Header("--- Componentes ---")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    [Header("--- Control ---")]
    private bool yaSeEnfado = false; // Para que solo se enfade una vez cuando el player se acerca
    private bool enfadadoPorEnemigos = false; // Para el enfado por enemigos muertos

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Busca al jugador automáticamente
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            jugador = playerObj.transform;
        else
            Debug.LogWarning("¡No se encontró objeto con tag 'Player'!");
    }

    private void Update()
    {
        if (jugador == null || yaSeEnfado) return;

        // Calcula distancia con el jugador
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador entra en rango, activa enfado
        if (distancia <= rangoDeteccion)
        {
            ActivarEnfado();
        }
    }

    private void ActivarEnfado()
    {
        yaSeEnfado = true;
        animator.SetTrigger("Enfado");
        
        // Opcional: Hacer que mire al jugador
        if (jugador != null)
            MirarJugador();
        
        Debug.Log("¡Boss enfadado por proximidad del jugador!");
    }

    private void MirarJugador()
    {
        if (transform.position.x < jugador.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    // Método público para que el GameManager2 lo llame cuando muere un enemigo
    public void TriggerAngerAnimation()
    {
        if (!enfadadoPorEnemigos)
        {
            enfadadoPorEnemigos = true;
            animator.SetTrigger("Enfado");
            Debug.Log("¡Boss enfadado por muerte de enemigo!");
        }
    }

    // Gizmos para ver el rango en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}


/*using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Posici�n final")]
    public Transform finalPosition; // Arrastra el punto final del mapa

    [Header("Movimiento")]
    public float moveSpeed = 2f;
    private bool hasReachedEnd = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (finalPosition != null && !hasReachedEnd)
        {
            // Mueve hacia el final del mapa
            transform.position = Vector3.MoveTowards(transform.position, finalPosition.position, moveSpeed * Time.deltaTime);

            // Para al llegar
            if (Vector3.Distance(transform.position, finalPosition.position) < 0.1f)
            {
                hasReachedEnd = true;
                // Opcional: Cambia a idle
                if (animator != null) animator.SetTrigger("Idle");
            }
        }
    }

    public void TriggerAngerAnimation()
    {
        if (animator != null && hasReachedEnd)
        {
            animator.SetTrigger("Enfado"); // Activa la animaci�n de enfado
        }
    }
}*/