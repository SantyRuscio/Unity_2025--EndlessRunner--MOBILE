using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagerr : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pausePanel; // Asigna el Panel de Pausa aquí
    [SerializeField] private GameObject gameHUD;    // Asigna el HUD (puntaje/vida) aquí para ocultarlo al pausar

    private bool isPaused = false;

    void Update()
    {
        // Esto funciona tanto para la tecla ESC en Windows 
        // como para el botón "Atrás" (Back button) nativo de Android.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Congela el tiempo (físicas y corrutinas)

        if (pausePanel != null) pausePanel.SetActive(true);
        if (gameHUD != null) gameHUD.SetActive(false);

        // Opcional: Pausar audios si molestan
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Devuelve el tiempo a la normalidad

        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameHUD != null) gameHUD.SetActive(true);

        AudioListener.pause = false;
    }

    public void GoToMainMenu(string menuSceneName)
    {
        Time.timeScale = 1f; // ¡CRÍTICO! Siempre devolver el tiempo a 1 antes de cambiar escena
        AudioListener.pause = false;
        SceneManager.LoadScene(menuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}