using UnityEngine;
using System.Collections;
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

    [SerializeField] private GameObject noStaminaPanel;

    private bool CanPlay = true;

    [SerializeField] private AsyncCharge asyncLoader;
    [SerializeField] private StaminaSystemWithNotifications stamina;

    private float ShowNoStaminaPanel = 2f;

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

        // Borrar TODOS los datos del save
        SaveManager.DeleteSave();

       // Resetear stamina también
       if (stamina != null)
           stamina.ResetStaminaSystem();

        Debug.Log("DATOS BORRADOS Y STAMINA RESETEADA DESDE EL MENU");

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

        // Chequeamos si tiene stamina
        if (stamina != null && stamina.HasEnoughStamina(1))
        {
            stamina.UseStamina(1);

            if (CanPlay && asyncLoader != null)
                asyncLoader.StartLevel("GamseScene");
            else if (CanPlay)
                SceneManager.LoadScene("GamseScene");
        }
        else
        {
            Debug.Log("NO tenés stamina suficiente.");

            // MOSTRAR PANEL
            if (noStaminaPanel != null)
                noStaminaPanel.SetActive(true);

            StartCoroutine(HideNoStaminaPanel());
        }
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

    private IEnumerator HideNoStaminaPanel()
    {
        yield return new WaitForSeconds(ShowNoStaminaPanel);
        if (noStaminaPanel != null)
            noStaminaPanel.SetActive(false);
    }
}
