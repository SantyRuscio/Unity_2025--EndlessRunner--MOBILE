using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;

    [SerializeField] private GameObject winPanel;

    void Start()
    {
        EventManager.Subscribe(TypeEcvents.GameOver, SetDefeatEnabled);

        EventManager.Subscribe(TypeEcvents.RewindEvent, SetRewimd);

        EventManager.Subscribe(TypeEcvents.Win, SetWinEnabled);
    }

    private void SetDefeatEnabled(params object[] parameters)
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true); 
            Debug.Log("UI de GameOver activada");
        }
    }
    private void SetRewimd(params object[] parameters)
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
            Debug.Log("UI de GameOver desactivada");
        }
    }

    private void SetWinEnabled(params object[] parameters)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("UI de Win activada");
        }
    }
    private void OnDestroy()
    {
        // IMPORTANTE: desuscribirse
        EventManager.Unsubscribe(TypeEcvents.GameOver, SetDefeatEnabled);

        EventManager.Unsubscribe(TypeEcvents.RewindEvent, SetRewimd);

        EventManager.Unsubscribe(TypeEcvents.Win, SetWinEnabled);

    }

}
