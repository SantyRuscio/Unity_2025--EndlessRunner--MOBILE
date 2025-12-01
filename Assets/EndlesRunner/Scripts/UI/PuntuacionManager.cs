using System.Collections;
using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager Instance;

    private int monedas = 0;               // Monedas del RUN actual
    private int monedasTotales = 0;        // Monedas guardadas en PlayerPrefs

    private float metrosRecorridos = 0f;

    private bool contadorActivo = true;
    private float multiplicadorMetros = 1f;
    private float multiplicadorMonedas = 1f;
    private Coroutine boostCoroutine;

    [Header("UI")]
    public TMP_Text textoMonedas;            // monedas del run
    public TMP_Text textoMetros;
    public TMP_Text textoMonedasTotales;     // opcional (tienda/menu)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Cargar monedas totales
        monedasTotales = PlayerPrefs.GetInt("MonedasTotales", 0);

        EventManager.Subscribe(TypeEvents.GameOver, StopCounter);
        EventManager.Subscribe(TypeEvents.RewindEvent, StartCounterRewind);
        EventManager.Subscribe(TypeEvents.Win, StopCounter);
        EventManager.Subscribe(TypeEvents.MultiplierEvent, ActivarMultiplicador);

        ActualizaHUDMonedas();
        ActualizaHUDMonedasTotales();
    }

    private void Update()
    {
        if (!contadorActivo) return;

        metrosRecorridos += Time.deltaTime * GameManager.instance.Speed * multiplicadorMetros;
        ActualizarHUDMetros();
    }

    #region// -------------------- MULTIPLICADOR --------------------
    public void ActivarMultiplicador(params object[] parameters)
    {
        if (parameters.Length < 2)
        {
            Debug.LogWarning("MultiplierEvent requiere 2 parámetros: (float multiplicador, float duración)");
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
        multiplicadorMonedas = nuevoMultiplicador; // <-- Nuevo: aplica a monedas
        yield return new WaitForSeconds(duracion);
        multiplicadorMetros = 1f;
        multiplicadorMonedas = 2f; // <-- Resetear después del tiempo
    }

    #endregion


    #region // -------------------- MONEDAS - METROS - PLAYER PREF --------------------
    public void AgregarMonedas(int cantidad)
    {
        if (!contadorActivo) return;

        monedas += Mathf.RoundToInt(cantidad * multiplicadorMonedas); // aplica multiplicador
        ActualizaHUDMonedas();
    }


    // Guarda solo al morir/ganar
    public void GuardarMonedasDelRun()
    {
        monedasTotales += monedas;

        PlayerPrefs.SetInt("MonedasTotales", monedasTotales);
        PlayerPrefs.Save();

        ActualizaHUDMonedasTotales();
    }

    public int GetMonedas() => monedas;
    public int GetMonedasTotales() => monedasTotales;
    public float GetMetrosRecorridos() => metrosRecorridos;

    private void ActualizaHUDMonedas()
    {
        if (textoMonedas != null)
            textoMonedas.text = monedas.ToString();
    }

    private void ActualizaHUDMonedasTotales()
    {
        if (textoMonedasTotales != null)
            textoMonedasTotales.text = monedasTotales.ToString();
    }

    private void ActualizarHUDMetros()
    {
        if (textoMetros != null)
            textoMetros.text = Mathf.FloorToInt(metrosRecorridos).ToString() + " m";
    }
    #endregion

    #region// -------------------- CONTROL --------------------
    private void StopCounter(params object[] parameters)
    {
        contadorActivo = false;

        // Guardar monedas del run al morir
        GuardarMonedasDelRun();
    }

    private void StartCounterRewind(params object[] parameters)
    {
        StartCoroutine(TimeToActivateCounter());
    }

    private IEnumerator TimeToActivateCounter()
    {
        yield return new WaitForSeconds(GameManager.RewindControlDelay);
        contadorActivo = true;
    }

    public void ReiniciarRun()
    {
        monedas = 0;
        metrosRecorridos = 0;

        ActualizaHUDMonedas();
        ActualizarHUDMetros();
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

