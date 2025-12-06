using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelEffectManagerr : MonoBehaviour
{
    public static PixelEffectManagerr Instance;

    [Header("Configuración")]
    [SerializeField] private Material materialPixel;
    [SerializeField] private float duracionEfecto = 8f;

    [Header("Valores del Glitch (Solo para Setup)")]
    [SerializeField] private Vector2 resolucionGlitch = new Vector2(640, 480);
    [SerializeField] private float lineas = 500f;
    [SerializeField] private float offset = 0.05f;

    private Coroutine corutinaActual;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ApagarEfecto();
    }

    private void Start()
    {
        if (materialPixel != null)
        {
            materialPixel.SetVector("_Resolucion", resolucionGlitch);
            materialPixel.SetFloat("_CantidadLineas", lineas);
            materialPixel.SetFloat("_Offset", offset);
            materialPixel.SetFloat("_OffsetNegativo", -offset);
        }
    }

    public void ActivarGlitch(float duracionPersonalizada = -1)
    {
        float tiempo = duracionPersonalizada > 0 ? duracionPersonalizada : duracionEfecto;

        if (corutinaActual != null) StopCoroutine(corutinaActual);
        corutinaActual = StartCoroutine(RutinaMasterSwitch(tiempo));
    }

    private IEnumerator RutinaMasterSwitch(float duracion)
    {
        materialPixel.SetFloat("_Poder", 1f);

        yield return new WaitForSeconds(duracion);

        ApagarEfecto();
    }

    public void ApagarEfecto()
    {
        if (materialPixel != null)
        {
            materialPixel.SetFloat("_Poder", 0f);
        }
    }

    private void OnApplicationQuit()
    {
        ApagarEfecto();
    }
}
