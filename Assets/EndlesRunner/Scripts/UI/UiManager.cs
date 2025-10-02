using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;

    [SerializeField] private GameObject winPanel;


    void Start()
    {
        EventManager.Subscribe(TypeEcvents.GameOver, SetDefeatEnabled);

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
    }

}
