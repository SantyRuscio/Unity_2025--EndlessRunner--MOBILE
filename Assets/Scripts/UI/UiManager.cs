using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;

    void Start()
    {
        EventManager.Subscribe(TypeEcvents.GameOver, SetDefeatEnabled);
    }

    private void SetDefeatEnabled(params object[] parameters)
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true); 
            Debug.Log("UI de GameOver activada");
        }
    }

    private void OnDestroy()
    {
        // IMPORTANTE: desuscribirse
        EventManager.Unsubscribe(TypeEcvents.GameOver, SetDefeatEnabled);
    }

}
