using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cinematica : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;


    // Start is called before the first frame update
    void Start()
    {
        Pause();
        textComponent.text = string.Empty;
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                
                NextLine();
                
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            
        }
        Resume();
    }

    

    public void Resume()
    {
        
        Time.timeScale = 1f; //Continua el juego
        GameIsPaused = false;
    }

    public void Pause()
    {
        
        Time.timeScale = 0f; //Pausa el juego
        GameIsPaused = true;
    }
}
