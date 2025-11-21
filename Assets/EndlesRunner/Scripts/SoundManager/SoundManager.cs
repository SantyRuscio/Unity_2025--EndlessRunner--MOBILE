using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source principal")]
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        // Singleton básico
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        Debug.Log("Reproducir Sonido");
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }
}

