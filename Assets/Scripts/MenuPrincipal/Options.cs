using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    private GameObject gameManager; //Almacena gameManager

    void Start()
    {
        gameManager = GameObject.Find("GameManager"); //Busca el gameobject llamado GameManager
        DontDestroyOnLoad(gameManager); //Preserva el objeto
    }

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Debug.Log("Pantalla Completa on"); // Avisa de que la pantalla completa funciona en el log
        Screen.fullScreen = pantallaCompleta; //Bool de pantalla completa
    }
}
    
   


