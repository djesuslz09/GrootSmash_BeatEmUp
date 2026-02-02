using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoBoss : MonoBehaviour
{
    [Header("--- Estadísticas ---")]
    [SerializeField] private float vida = 500f;
    
    [Header("--- IA Combate ---")]
    private Transform jugador;
    [SerializeField] private float rangoDeteccion = 8f;
    [SerializeField] private float rangoAtaque = 2f;
    [SerializeField] private float damageAtaque = 20f;
    [SerializeField] private float tiempoEntreAtaques = 3f;

    [Header("--- Proyectiles ---")]
    [SerializeField] private GameObject proyectilPrefab; // Prefab del tronco
    [SerializeField] private Transform puntoDisparo; // De dónde salen los troncos
    [SerializeField] private float velocidadProyectil = 5f;

    private float cronometroAtaque;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool estaMuerto = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Busca al jugador automáticamente
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            jugador = playerObj.transform;
        else
            Debug.LogWarning("¡No se encontró jugador con tag 'Player'!");
    }

    private void Update()
    {
        if (estaMuerto || jugador == null) return;

        ComportamientoBoss();
    }

    private void ComportamientoBoss()
    {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia < rangoDeteccion)
        {
            MirarJugador();

            if (distancia <= rangoAtaque)
            {
                if (Time.time >= cronometroAtaque)
                {
                    Atacar();
                    cronometroAtaque = Time.time + tiempoEntreAtaques;
                }
            }
        }
    }

    private void MirarJugador()
    {
        if (transform.position.x < jugador.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    private void Atacar()
    {
        animator.SetTrigger("Atacar");
        LanzarProyectil();
    }

    private void LanzarProyectil()
    {
        if (proyectilPrefab == null || puntoDisparo == null || jugador == null) return;

        // Instancia el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
        
        // Calcula dirección hacia el jugador
        Vector2 direccion = (jugador.position - puntoDisparo.position).normalized;
        
        // Aplica velocidad al proyectil
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direccion * velocidadProyectil;
        }
    }

    public void TomarDamage(float damage)
    {
        if (estaMuerto) return;

        vida -= damage;
        Debug.Log("Boss recibe daño. Vida restante: " + vida);

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        estaMuerto = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
        Debug.Log("¡Boss derrotado! Reiniciando nivel...");
        
        StartCoroutine(MuerteYReinicio());
    }

    private IEnumerator MuerteYReinicio()
    {
        // Fade a rojo
        float duracion = 0.5f;
        float tiempo = 0f;
        Color colorInicial = spriteRenderer.color;
        Color colorFinal = Color.red;
        
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(colorInicial, colorFinal, tiempo / duracion);
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}