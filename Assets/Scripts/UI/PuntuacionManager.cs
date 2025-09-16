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

    [Header("Refes")]
    public Transform jugador;

    [Header("UI")]

    public TMP_Text textoMonedas; 

    public TMP_Text textoMetros;  

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        if (jugador != null)
            ultimaPosicionJugador = jugador.position;

        ActualizarUI();
    }

    private void Update()
    {
        if (jugador != null)
        {
            float distanciaFrame = Vector3.Distance(jugador.position, ultimaPosicionJugador);
            metrosRecorridos += distanciaFrame;
            ultimaPosicionJugador = jugador.position;


            ActualizarUI();
        }
    }

    public void AgregarMonedas(int cantidad)
    {
        monedas += cantidad;
        ActualizarUI();
    }

    public int GetMonedas()
    {
        return monedas;
    }

    public float GetMetrosRecorridos()
    {
        return metrosRecorridos;
    }

    private void ActualizarUI()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = monedas.ToString();
        }

        if (textoMetros != null)
        {
            textoMetros.text = Mathf.FloorToInt(metrosRecorridos).ToString() + " m";
        }
    }
}