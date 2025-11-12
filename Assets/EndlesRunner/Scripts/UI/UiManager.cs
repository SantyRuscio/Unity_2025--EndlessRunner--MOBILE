using UnityEngine;

public class UiManager : MonoBehaviour, IScreen
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject winPanel;

    private void Start()
    {
        EventManager.Subscribe(TypeEvents.GameOver, SetDefeatEnabled);
        EventManager.Subscribe(TypeEvents.RewindEvent, SetRewimd);
        EventManager.Subscribe(TypeEvents.Win, SetWinEnabled);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEvents.GameOver, SetDefeatEnabled);
        EventManager.Unsubscribe(TypeEvents.RewindEvent, SetRewimd);
        EventManager.Unsubscribe(TypeEvents.Win, SetWinEnabled);
    }

    #region ISCREEN
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    #endregion

    private void SetDefeatEnabled(params object[] parameters)
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
            Debug.Log("UI de GameOver activada");

            ScreenManager.Instance.ActivateScreen(this);
        }
    }

    private void SetRewimd(params object[] parameters)
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
            Debug.Log("UI de GameOver desactivada");

            ScreenManager.Instance.DesactivateScreen();
        }
    }

    private void SetWinEnabled(params object[] parameters)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("UI de Win activada");

            ScreenManager.Instance.ActivateScreen(this);
        }
    }
}

