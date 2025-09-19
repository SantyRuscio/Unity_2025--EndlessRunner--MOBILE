using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Butons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Settings")]
    public AudioSource AudioSource;
    public AudioClip ClickClip;
    public AudioClip HoverClip;

    private void SetClip(AudioClip clip)
    {
        if (AudioSource != null && clip != null)
        {
            AudioSource.PlayOneShot(clip);
        }
    }

    public void GoToMenu()
    {
        SetClip(ClickClip);
        // SceneManager.LoadScene("MenuDeInicio");
    }

    public void RestartLevel()
    {
        SetClip(ClickClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // public void ShowCredits()
    // {
    //     SetClip(ClickClip);
    //     if (Credits != null)
    //         Credits.SetActive(true);
    // }

    public void QuitGame()
    {
        SetClip(ClickClip);
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetClip(HoverClip);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetClip(ClickClip);
    }
}