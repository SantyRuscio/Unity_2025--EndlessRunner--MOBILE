using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DeathPanelInfo : MonoBehaviour
{
    [Header("Referencias a los textos del panel de muerte")]
    [SerializeField] private TMP_Text monedasRunText;
    [SerializeField] private TMP_Text distanciaRunText;
    [SerializeField] private TMP_Text recordDistanciaText;

    private void OnEnable()
    {
        if (PuntuacionManager.Instance == null) return;

        // Monedas del run
        int monedasRun = PuntuacionManager.Instance.GetMonedas();
        if (monedasRunText != null)
            monedasRunText.text = monedasRun.ToString();

        // Distancia del run
        float metrosRun = PuntuacionManager.Instance.GetMetrosRecorridos();
        if (distanciaRunText != null)
            distanciaRunText.text = Mathf.FloorToInt(metrosRun) + " m";

        // Record global
        float record = PlayerPrefs.GetFloat("RecordDistancia", 0f);
        if (recordDistanciaText != null)
            recordDistanciaText.text = Mathf.FloorToInt(record) + " m";
    }
}


