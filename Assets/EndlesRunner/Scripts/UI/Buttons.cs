using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Butons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Audio Settings")]
    public AudioSource AudioSource;
    public AudioClip ClickClip;
    public AudioClip HoverClip;

    [Header("Delete Data Settings")] 
    [SerializeField] private GameObject confirmationPanel;

    private bool CanPlay = true;

    [SerializeField] private AsyncCharge asyncLoader;

    private void Start()
    {
        if (confirmationPanel != null) confirmationPanel.SetActive(false);

        if (RemoteConfigExample.Instance != null)
        {
            CanPlay = RemoteConfigExample.Instance.gameActivate;
        }
    }

    private void SetClip(AudioClip clip)
    {
        if (AudioSource != null && clip != null)
            AudioSource.PlayOneShot(clip);
    }

    public void OpenConfirmation()
    {
        if (confirmationPanel != null)
        {
            SetClip(ClickClip);
            confirmationPanel.SetActive(true);
        }
    }

    public void CloseConfirmation()
    {
        if (confirmationPanel != null)
        {
            SetClip(ClickClip);
            confirmationPanel.SetActive(false);
        }
    }
    public void ConfirmDelete()
    {
        SetClip(ClickClip); 

        SaveManager.DeleteSave();

        Debug.Log("DATOS BORRADOS DESDE EL MENU");

        CloseConfirmation();

    }

    public void GoToMenu()
    {
        DeathEffectController deathFX = FindObjectOfType<DeathEffectController>();
        if (deathFX != null)
        {
            deathFX.HideDeathScreen();
        }
        SetClip(ClickClip);
        SceneManager.LoadScene("MenuDeInicio");
    }

    public void GoToGame()
    {
        SetClip(ClickClip);

        if (CanPlay && asyncLoader != null)
            asyncLoader.StartLevel("GamseScene");
        else if (CanPlay)
            SceneManager.LoadScene("GamseScene");
    }

    public void RestartLevel()
    {
        if (PuntuacionManager.Instance != null)
            PuntuacionManager.Instance.ReiniciarRun();

        DeathEffectController deathFX = FindObjectOfType<DeathEffectController>();
        if (deathFX != null)
        {
            deathFX.HideDeathScreen();
        }

        SetClip(ClickClip);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevelWithVideo()
    {

        DeathEffectController deathFX = FindObjectOfType<DeathEffectController>();
        if (deathFX != null)
        {
            deathFX.HideDeathScreen();
        }

        SetClip(ClickClip);

        GameManager.instance.LoadMethod();

        AdsManager.Instance.ShowRewardedAd();
    }

    public void QuitGame()
    {
        DeathEffectController deathFX = FindObjectOfType<DeathEffectController>();
        if (deathFX != null)
        {
            deathFX.HideDeathScreen();
        }

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
