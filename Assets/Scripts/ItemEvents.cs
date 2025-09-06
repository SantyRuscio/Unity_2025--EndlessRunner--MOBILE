using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemEvents
{
    // eventos estaticos
    public static event Action<int> OnMonedaRecolectada;
    public static event Action<PowerUp.TipoPowerUp, float> OnPowerUpRecolectado;
    public static event Action<int> OnObstaculoGolpeado;

    // invoacion de eventos
    public static void MonedaRecolectada(int puntos)
    {
        OnMonedaRecolectada?.Invoke(puntos);
    }

    public static void PowerUpRecolectado(PowerUp.TipoPowerUp tipo, float duracion)
    {
        OnPowerUpRecolectado?.Invoke(tipo, duracion);
    }

    public static void ObstaculoGolpeado(int daño)
    {
        OnObstaculoGolpeado?.Invoke(daño);
    }
}
