using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController_Props_Prueba : MonoBehaviour
{
    public float speedH = 5f;
    public float speedV = 5f;
 

    //Para cambiar entre estados en el animator
    public Rigidbody2D rb2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer; // arrastarrlo en el isnpector

    //Prop vida
    public int vidaActual = 50;
    public int vidaMaxima = 100;

    //Prop ataque
    public int dañoAtaque = 10;


    //Prop defensa






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

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }

        Debug.Log("¡Jugador curado! Vida actual: " + vidaActual);
    }
    public void IncreaseDamage(int cantidad)
    {
        dañoAtaque += cantidad;
        Debug.Log("¡Fuerza aumentada! Daño actual: " + dañoAtaque);
    }
}
