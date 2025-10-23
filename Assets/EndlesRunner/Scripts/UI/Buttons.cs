using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Butons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Settings")]
    public AudioSource AudioSource;
    public AudioClip ClickClip;
    public AudioClip HoverClip;

    private bool CanPlay = true;

    private void Start()
    {
        // inicializar con los valores de RemoteConfig
        if (RemoteConfigExample.Instance != null)
        {
            CanPlay = RemoteConfigExample.Instance.gameActivate;
        }
    }


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
        SceneManager.LoadScene("MenuDeInicio");
    }

    public void GoToGame()
    {
        SetClip(ClickClip);
        if (CanPlay)
        {
            SceneManager.LoadScene("GamseScene");
        }
    }

    public void RestartLevel()
    {
        SetClip(ClickClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevelWithVideo()
    {
        SetClip(ClickClip);
       // GameManager.instance.LoadMethod();

        EventManager.Trigger(TypeEcvents.RewindEvent);
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