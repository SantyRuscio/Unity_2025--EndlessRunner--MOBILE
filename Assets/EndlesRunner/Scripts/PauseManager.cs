using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManagerr : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pausePanel;
    public GameObject gameHUD;

    [Header("Referencias Audio")]
    public Slider volumeSlider; 

    private bool isPaused = false;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumenGlobal", 1f);

        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;

            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //comentario para santi: esto sirve para poner pausa con el esc o el "back" del celu, aparte de con el boton de UI
        {                                         
            TogglePause();                        
        }                                         
    }

    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null) pausePanel.SetActive(true);
        if (gameHUD != null) gameHUD.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameHUD != null) gameHUD.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeVolume(float valor)
    {                                                                       //volumen persistente aunque cierres el jueguito
        AudioListener.volume = valor;                                       //volumen persistente aunque cierres el jueguito
        PlayerPrefs.SetFloat("VolumenGlobal", valor);                       //volumen persistente aunque cierres el jueguito
        PlayerPrefs.Save();                                                 //volumen persistente aunque cierres el jueguito
    }                                                                       //volumen persistente aunque cierres el jueguito
}