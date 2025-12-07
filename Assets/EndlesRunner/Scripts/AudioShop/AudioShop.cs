using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShop : MonoBehaviour
{
    public static AudioShop Instance;

    [Header("Audio Source")]
    public AudioSource audioSource; 
    public AudioClip[] musicClips;  

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadMusic();
    }

    // Reproducir música usando índice de array
    public void PlayMusic(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= musicClips.Length) return;

        audioSource.clip = musicClips[clipIndex];
        audioSource.Play();

        // Guardamos la elección
        PlayerPrefs.SetInt("SelectedMusic", clipIndex);
        PlayerPrefs.Save();
    }

    // Reproducir música usando un AudioClip
    public void PlayMusicClip(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();

        PlayerPrefs.SetString("SelectedMusicName", clip.name);
        PlayerPrefs.Save();
    }

    // Cargar música guardada al iniciar el juego
    public void LoadMusic()
    {
        int savedClip = PlayerPrefs.GetInt("SelectedMusic", 0); // 0 por defecto
        PlayMusic(savedClip);
    }

}


