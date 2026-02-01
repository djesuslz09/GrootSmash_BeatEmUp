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


}
