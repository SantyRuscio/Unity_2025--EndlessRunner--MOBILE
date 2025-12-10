using System.Collections;
using UnityEngine;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    public static PuntuacionManager Instance;

    [Header("Monedas")]
    private int monedas = 0;
    private int monedasTotales = 0;

    [Header("Metros")]
    private float metrosTotales = 0f;
    private float metrosRecorridos = 0f;
    private bool contadorActivo = true;

    private float multiplicadorMetros = 1f;
    private float multiplicadorMonedas = 1f;
    private Coroutine boostCoroutine;

    [Header("UI")]
    public TMP_Text textoMonedas;
    public TMP_Text textoMetros;
    public TMP_Text textoMonedasTotales;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        monedasTotales = PlayerPrefs.GetInt("MonedasTotales", 0);
        metrosTotales = PlayerPrefs.GetFloat("MetrosTotales", 0f);

        EventManager.Subscribe(TypeEvents.GameOver, StopCounter);
        EventManager.Subscribe(TypeEvents.RewindEvent, StartCounterRewind);
        EventManager.Subscribe(TypeEvents.Win, StopCounter);
        EventManager.Subscribe(TypeEvents.MultiplierEvent, ActivarMultiplicador);
        EventManager.Subscribe(TypeEvents.DefubCrabEvent, CancelarMultiplicador);

        ActualizaHUDMonedas();
        ActualizaHUDMonedasTotales();
    }

    private void Update()
    {
        if (!contadorActivo) return;

        metrosRecorridos += Time.deltaTime * GameManager.instance.Speed * multiplicadorMetros;
        ActualizarHUDMetros();
    }

    #region MULTIPLICADOR
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
        multiplicadorMonedas = nuevoMultiplicador;
        yield return new WaitForSeconds(duracion);
        multiplicadorMetros = 1f;
        multiplicadorMonedas = 1f;
        boostCoroutine = null;
    }

    public void CancelarMultiplicador(object[] parameters = null)
    {
        if (boostCoroutine != null)
        {
            StopCoroutine(boostCoroutine);
            boostCoroutine = null;
        }

        multiplicadorMetros = 1f;
        multiplicadorMonedas = 1f;
    }
    #endregion

    #region MONEDAS - METROS - PLAYER PREF
    public void AgregarMonedas(int cantidad)
    {
        if (!contadorActivo) return;

        monedas += Mathf.RoundToInt(cantidad * multiplicadorMonedas);
        ActualizaHUDMonedas();
    }

    public void GuardarMonedasDelRun()
    {
        monedasTotales += monedas;

        PlayerPrefs.SetInt("MonedasTotales", monedasTotales);
        PlayerPrefs.Save();

        ActualizaHUDMonedasTotales();
    }

    public void GuardarMetrosDelRun()
    {
        metrosTotales += metrosRecorridos;
        PlayerPrefs.SetFloat("MetrosTotales", metrosTotales);

        // ✅ GUARDAR RECORD SI SUPERA EL ANTERIOR
        float recordActual = PlayerPrefs.GetFloat("RecordDistancia", 0f);

        if (metrosRecorridos > recordActual)
        {
            PlayerPrefs.SetFloat("RecordDistancia", metrosRecorridos);
        }

        PlayerPrefs.Save();
    }

    public int GetMonedas() => monedas;
    public int GetMonedasTotales() => monedasTotales;
    public float GetMetrosRecorridos() => metrosRecorridos;
    public float GetMetrosTotales() => metrosTotales;

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
            textoMetros.text = Mathf.FloorToInt(metrosRecorridos) + " m";
    }
    #endregion

    #region CONTROL
    private void StopCounter(params object[] parameters)
    {
        contadorActivo = false;
        GuardarMonedasDelRun();
        GuardarMetrosDelRun();
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
        EventManager.Unsubscribe(TypeEvents.DefubCrabEvent, CancelarMultiplicador);
    }
}
