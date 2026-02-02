using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public CharacterWithAmbience characterAudio;


    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Pulsar Escape para abrir el men�
        {
            if (GameIsPaused) 
            {
                Resume(); //Si pulsas escape en el juego, continua
            } else
            {
                Pause(); //Si no es el juego, se pausa
            }
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //Continua el juego
        GameIsPaused = false;
        // Reanudar música 
        if (characterAudio != null) characterAudio.ResumeAudio();
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Pausa el juego
        GameIsPaused = true;
        // Pausar música 
        if (characterAudio != null) characterAudio.PauseAudio();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Inicio");
        Debug.Log("Cargando menu");
    }

    public void QuitGame() //Para salir del juego, no lo he puesto en el men� pero por si acaso
    {
        Debug.Log("Cerrar el juego");
    }
}
