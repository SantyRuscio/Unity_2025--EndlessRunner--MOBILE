using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager Instance;

    private int monedas = 0;
    private float metrosRecorridos = 0f;
    private Vector3 ultimaPosicionJugador;

    private bool contadorActivo = true; 

    [Header("Refes")]
    public Transform jugador;

    [Header("UI")]
    public TMP_Text textoMonedas;
    public TMP_Text textoMetros;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        EventManager.Subscribe(TypeEcvents.GameOver, StopCounter);

        if (jugador != null)
            ultimaPosicionJugador = jugador.position;

        ActualizarUI();
    }

    private void Update()
    {
        if (!contadorActivo) return;
        metrosRecorridos += Time.deltaTime * 5f;
        ActualizarUI();
    }

    public void AgregarMonedas(int cantidad)
    {
        if (!contadorActivo) return; 

        monedas += cantidad;
        ActualizarUI();
    }

    public int GetMonedas() => monedas;
    public float GetMetrosRecorridos() => metrosRecorridos;

    private void ActualizarUI()
    {
        if (textoMonedas != null)
            textoMonedas.text = monedas.ToString();

        if (textoMetros != null)
            textoMetros.text = Mathf.FloorToInt(metrosRecorridos).ToString() + " m";
    }

    private void StopCounter(params object[] parameters)
    {
        contadorActivo = false; 
    }
}
