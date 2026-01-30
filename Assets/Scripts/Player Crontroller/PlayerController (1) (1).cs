using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class prueba : MonoBehaviour
{
    public float speedH = 5f;
    public float speedV = 5f;
    
    

    //Para cambiar entre estados en el animator
    public Rigidbody2D rb2D;
    public Animator animator;
    public SpriteRenderer spriteRenderer; // arrastarrlo en el isnpector

    //Referencia a la camara
    public Camera mainCamera;

    private void Start()
    {
        //Buscar cámara automaticamente si no esta asignada
        if(mainCamera == null)
            mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        //Input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        //Movimiento sin suavizado/deslizamiento
        Vector3 move = new Vector3(inputX * speedH, inputY * speedV, 0f);
        transform.position += move * Time.deltaTime;

        //Limitador: no pasa de la mitad de la pantalla
        LimitarPantalla();

        //Animaciones (Detecta movimiento)
        bool isMoving = (inputX != 0 || inputY != 0);
        animator.SetBool("IsMoving", isMoving);

        //Girar sprite

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

    //Limitador de pantalla
    void LimitarPantalla()
    {
        Vector3 pos = transform.position;

        //Altura visible de la camara
        float halfHeight = mainCamera.orthographicSize;

        //Limitar Y: desde arriba (0) hasta la mitad de la pantalla
        pos.y = Mathf.Clamp(pos.y, -halfHeight, 0f);

        transform.position = pos;
    }
}
