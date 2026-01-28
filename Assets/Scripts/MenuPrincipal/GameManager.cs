using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private GameObject gameManager; //Almacena gameManager
    [SerializeField] private AudioMixer audioMixer; //Almacena audioMixer

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager"); //Busca el gameobject llamado GameManager
        DontDestroyOnLoad(gameManager); //Preserva el objeto
    }

    
    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
