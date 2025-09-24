using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager Instance;

    private int monedas = 0;
    private float metrosRecorridos = 0f;

    private bool contadorActivo = true; 

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
    }

    private void Update()
    {
        if (!contadorActivo) return;    
        metrosRecorridos += Time.deltaTime * GameManager.instance.Speed; //esto paara que sume mas rapido los metros 
        ActualizarHUDMetros();
    }

    public void AgregarMonedas(int cantidad)
    {
        if (!contadorActivo) return; 

        monedas += cantidad;
        ActualizaHUDMOnedas();
    }

    public int GetMonedas() => monedas;
    public float GetMetrosRecorridos() => metrosRecorridos;

    private void ActualizaHUDMOnedas()
    {
        if (textoMonedas != null)
            textoMonedas.text = monedas.ToString();
    }

    private void ActualizarHUDMetros()
    {  
        if (textoMetros != null)
            textoMetros.text = Mathf.FloorToInt(metrosRecorridos).ToString() + " m";
    }

    private void StopCounter(params object[] parameters)
    {
        contadorActivo = false; 
    }
}
