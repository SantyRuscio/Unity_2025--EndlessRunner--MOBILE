using System.Collections;
using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager Instance;

    private int monedas = 0;
    private float metrosRecorridos = 0f;

    private bool contadorActivo = true;
    private float multiplicadorMetros = 1f; // 🔹 Por defecto, normal (x1)
    private Coroutine boostCoroutine;       // 🔹 Para manejar el power-up activo

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
        EventManager.Subscribe(TypeEvents.GameOver, StopCounter);
        EventManager.Subscribe(TypeEvents.RewindEvent, StartCounterRewind);
        EventManager.Subscribe(TypeEvents.Win, StopCounter);
        EventManager.Subscribe(TypeEvents.MultiplierEvent, ActivarMultiplicador);
    }

    private void Update()
    {
        if (!contadorActivo) return;

        // Suma los metros recorridos considerando el multiplicador
        metrosRecorridos += Time.deltaTime * GameManager.instance.Speed * multiplicadorMetros;
        ActualizarHUDMetros();
    }


    #region  // ACTIVADOR DE MULTIPLICADOR DE METROS
    public void ActivarMultiplicador(params object[] parameters)
    {
        // 🔹 Verificamos que lleguen los parámetros correctos
        if (parameters.Length < 2)
        {
            Debug.LogWarning(" MultiplierEvent requiere 2 parámetros: (float multiplicador, float duración)");
            return;
        }

        float nuevoMultiplicador = (float)parameters[0];
        float duracion = (float)parameters[1];

        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(MultiplicadorTemporal(nuevoMultiplicador, duracion));
    }

    private IEnumerator MultiplicadorTemporal(float nuevoMultiplicador, float duracion)
    {
        multiplicadorMetros = nuevoMultiplicador;
        Debug.Log($"🟢 Multiplicador de metros activado: x{nuevoMultiplicador} por {duracion} segundos");
        yield return new WaitForSeconds(duracion);
        multiplicadorMetros = 1f;
        Debug.Log("Multiplicador de metros finalizado");
    }
    #endregion


    #region // MANEJO DE MONEDAS Y HUD
    public void AgregarMonedas(int cantidad)
    {
        if (!contadorActivo) return;

        monedas += cantidad;
        ActualizaHUDMonedas();
    }

    public int GetMonedas() => monedas;
    public float GetMetrosRecorridos() => metrosRecorridos;

    private void ActualizaHUDMonedas()
    {
        if (textoMonedas != null)
            textoMonedas.text = monedas.ToString();
    }

    private void ActualizarHUDMetros()
    {
        if (textoMetros != null)
            textoMetros.text = Mathf.FloorToInt(metrosRecorridos).ToString() + " m";
    }
    #endregion


    #region // EVENTOS DE CONTROL
    private void StopCounter(params object[] parameters)
    {
        contadorActivo = false;
    }

    private void StartCounterRewind(params object[] parameters)
    {
        StartCoroutine(TimeToActivateCounter());
    }

    private IEnumerator TimeToActivateCounter()
    {
        yield return new WaitForSeconds(6f);
        contadorActivo = true;
        Debug.Log(" Contador reactivado después del rewind");
    }
    #endregion


    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEvents.GameOver, StopCounter);
        EventManager.Unsubscribe(TypeEvents.RewindEvent, StartCounterRewind);
        EventManager.Unsubscribe(TypeEvents.Win, StopCounter);
        EventManager.Unsubscribe(TypeEvents.MultiplierEvent, ActivarMultiplicador);
    }
}
