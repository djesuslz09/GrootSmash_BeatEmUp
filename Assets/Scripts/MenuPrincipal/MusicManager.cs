using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource musicSource; //acceso a la librería de música

    private void Awake() // comprueba que el gameObj de música no se duplica
    {
        if (Instance != null)
        {
            Destroy(gameObject); // si se duplica, lo destruye
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // si no se duplica, lo mantiene
        }
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f) //acceso a la música mediante la librería, compartiendo la duración
    {
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }

    // efecto de fade de musica
    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }


}
