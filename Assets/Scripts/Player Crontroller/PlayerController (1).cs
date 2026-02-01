using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // llamar a la función para los props ( defensa y vida
    public int health = 100;
    public int defense = 0;


    public void IncreaseHealth(int amount)
    {
        health += amount;
        Debug.Log("Vida aumentada: " + health);
    }

    public void IncreaseDefense(int amount)
    {
        defense += amount; 
        Debug.Log("Defensa aumentada: " +  defense);
    }





    [Header("--- Movimiento ---")]
    [SerializeField] private float speedH = 5f;
    [SerializeField] private float speedV = 5f;
    
    [Header("--- Componentes ---")]
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float vidaJugador = 100f;
    private float vidaMaxima = 100f;

    private void Start()
    {
        // Obtenemos los componentes automáticamente si están en el mismo objeto
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ProcesarMovimiento();
        //ProcesarCombate();
    }

    private void ProcesarMovimiento()
    {
        // 1. Input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // 2. Movimiento (Usando transform como en tu script original)
        Vector3 move = new Vector3(inputX * speedH, inputY * speedV, 0f);
        transform.position += move * Time.deltaTime;

        // 3. Animaciones de movimiento
        bool isMoving = (inputX != 0 || inputY != 0);
        animator.SetBool("IsMoving", isMoving);

        // 4. Girar Sprite (Flip)
        if (inputX > 0)
            spriteRenderer.flipX = false; // Derecha
        else if (inputX < 0)
            spriteRenderer.flipX = true;  // Izquierda
    }
    
    public void RecibirDaño(float cantidad)
    {
        vidaJugador -= cantidad;
        vidaJugador = Mathf.Clamp(vidaJugador, 0, vidaMaxima); // Evita vida negativa
        
        Debug.Log("Jugador herido. Vida actual: " + vidaJugador);

        if (vidaJugador <= 0)
        {
            MuerteJugador();
        }
    }
    private void MuerteJugador()
    {
        animator.SetTrigger("Muerte");
        this.enabled = false; // Desactiva movimiento
        
        // Espera 2 segundos y reinicia la escena
        Invoke("ReiniciarNivel", 2f);
    }
    private void ReiniciarNivel()
    {
        // Carga la escena actual desde el principio
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public float ObtenerVida() => vidaJugador;
}

/*public class prueba : MonoBehaviour
{
    public float speedH = 5f;
    public float speedV = 5f;
    
    

    //Para cambiar entre estados en el animator
    public Rigidbody2D rb2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer; // arrastarrlo en el isnpector

    
    

    // Update is called once per frame
    void Update()
    {
        //Input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        //Movimiento sin suavizado/deslizamiento
        Vector3 move = new Vector3(inputX * speedH, inputY * speedV, 0f);
        transform.position += move * Time.deltaTime;

        //Animaciones (Detecta movimiento)
        bool isMoving = (inputX != 0 || inputY != 0);
        animator.SetBool("IsMoving", isMoving);

        //Girar sprte

        if (inputX > 0)
            spriteRenderer.flipX = false; //Mirando a la derecha
        else if (inputX < 0)
            spriteRenderer.flipX = true; //Mirando a la izquierda

        //Atack (Detecta Ataque)
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
        }
        //animator.SetFloat("Velocidad", rbSpeed); //Para cambiar entre estados en el animator

    }

}*/
