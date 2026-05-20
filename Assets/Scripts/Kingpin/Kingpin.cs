using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAngryController : MonoBehaviour
{
    [Header("--- Vida ---")]
    [SerializeField] private float vida = 500f;

    [Header("--- Detección Jugador ---")]
    private Transform jugador;
    [SerializeField] private float rangoDeteccion = 10f;

    [Header("--- Proyectiles ---")]
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float velocidadProyectil = 5f;
    [SerializeField] private float tiempoEntreDisparos = 3f;

    private float cronometroDisparo;
    private bool estaMuerto = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cronometroDisparo = Time.time + tiempoEntreDisparos;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            jugador = playerObj.transform;
        else
            Debug.LogWarning("¡No se encontró jugador con tag 'Player'!");
    }

    void Update()
    {
        if (estaMuerto || jugador == null) return;

        MirarJugador();

        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoDeteccion)
        {
            if (Time.time >= cronometroDisparo)
            {
                LanzarProyectil();
                cronometroDisparo = Time.time + tiempoEntreDisparos;
            }
        }
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

    public void TomarDamage(float damage)
    {
        if (estaMuerto) return;

        vida -= damage;
        Debug.Log("Boss recibe daño. Vida restante: " + vida);

        if (vida <= 0)
            Muerte();
    }

    private void Muerte()
    {
        estaMuerto = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Debug.Log("¡Boss derrotado!");
        StartCoroutine(MuerteYReinicio());
    }

    private IEnumerator MuerteYReinicio()
    {
        float duracion = 0.5f;
        float tiempo = 0f;
        Color colorInicial = spriteRenderer.color;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(colorInicial, Color.red, tiempo / duracion);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnEnemyKilled()
    {
        Debug.Log("Boss enfadado. Enemigos muertos.");
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