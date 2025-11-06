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

    [SerializeField] private AsyncCharge asyncLoader;

    private void Start()
    {
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

    public void GoToMenu()
    {
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
        SetClip(ClickClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevelWithVideo()
    {
        SetClip(ClickClip);
        AdsManager.Instance.ShowRewardedAd();
    }

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
