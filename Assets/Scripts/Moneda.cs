using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : Item
{
    [Header("Configuración Moneda")]
    public int puntos = 10;

    void Start()
    {
        nombreItem = "Moneda";
        valorPuntos = puntos;
    }

    public override void AplicarEfecto()
    {
        ItemEvents.MonedaRecolectada(valorPuntos);
        Debug.Log($"¡Moneda recolectada! +{valorPuntos} puntos");
    }
}