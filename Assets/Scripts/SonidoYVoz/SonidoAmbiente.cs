using UnityEngine;

public class CharacterWithAmbience : MonoBehaviour
{
    [Header("Voz del personaje")]
    public AudioClip voiceLine;
    public float voiceVolume = 1f;

    [Header("Sonido de ambiente")]
    public AudioClip ambienceClip;
    public float ambienceVolume = 0.5f;

    private AudioSource voiceSource;
    private AudioSource ambienceSource;

    void Awake()
    {
        // AudioSource para la voz
        voiceSource = gameObject.AddComponent<AudioSource>();
        voiceSource.loop = false;
        voiceSource.volume = voiceVolume;

        // AudioSource para el ambiente
        ambienceSource = gameObject.AddComponent<AudioSource>();
        ambienceSource.loop = true;
        ambienceSource.volume = ambienceVolume;
    }

    void Start()
    {
        // Iniciar ambiente
        if (ambienceClip != null)
        {
            ambienceSource.clip = ambienceClip;
            ambienceSource.Play();
        }
    }

    public void SayLine()
    {
        if (voiceLine != null)
        {
            voiceSource.clip = voiceLine;
            voiceSource.Play();
        }
        else
        {
            Debug.LogWarning("No hay línea de voz asignada.");
        }
    }

    //  función que solo reproduce la voz a veces
    public void SayLineSometimes(float probability = 0.4f)
    {
        // probability = 0.4 → 40% de probabilidad
        if (Random.value <= probability)
        {
            SayLine();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            // Aquí antes llamabas a SayLine()
            // Ahora llamamos a la versión con probabilidad
            SayLineSometimes(0.4f); // 40% de las veces
            Debug.Log("J presionada");

        }
    }
}




