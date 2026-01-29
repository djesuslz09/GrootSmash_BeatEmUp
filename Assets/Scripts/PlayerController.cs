using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class prueba : MonoBehaviour
{
    public float speedH = 5f;
    public float speedV = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        //Movimiento sin suavizado/deslizamiento
        Vector3 move = new Vector3(inputX * speedH, inputY * speedV, 0f);

        transform.position += move * Time.deltaTime;

    }

}
