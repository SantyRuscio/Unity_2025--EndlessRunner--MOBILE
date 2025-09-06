using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuntuacionManager : MonoBehaviour
{
    [Header("configuracion UI")]
    public TMP_Text textoPuntuacion; // aca va el TMPro con el texto del score

    [Header("Estadísticas")]
    public int puntuacionTotal = 0;
    public int monedasRecolectadas = 0;
    public static PuntuacionManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ActualizarUI();
    }

    void OnEnable()
    {
        ItemEvents.OnMonedaRecolectada += AgregarPuntos;
    }

    void OnDisable()
    {
        ItemEvents.OnMonedaRecolectada -= AgregarPuntos;
    }

    public void AgregarPuntos(int puntos)
    {
        puntuacionTotal += puntos;
        monedasRecolectadas++;

        ActualizarUI();
        Debug.Log($"+{puntos} puntos | Total: {puntuacionTotal}");
    }

    private void ActualizarUI()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"Puntos: {puntuacionTotal}";
        }
    }

    // guardar/cargar scores (posible feature)
    public void GuardarHighScore()
    {
        PlayerPrefs.SetInt("HighScore", puntuacionTotal);
        PlayerPrefs.Save();
    }

    public int CargarHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    //reiniciar score

    public void ReiniciarPuntuacion()
    {
        puntuacionTotal = 0;
        monedasRecolectadas = 0;
        ActualizarUI();
    }

    // para actualizar desde otros script
    public void ActualizarTextoPuntuacion(TMP_Text nuevoTexto)
    {
        textoPuntuacion = nuevoTexto;
        ActualizarUI();
    }
}
