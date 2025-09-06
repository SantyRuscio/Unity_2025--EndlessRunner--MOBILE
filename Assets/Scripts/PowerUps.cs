using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    public enum TipoPowerUp { Escudo, Iman }

    [Header("Configuración PowerUp")]
    public TipoPowerUp tipoPowerUp = TipoPowerUp.Escudo;
    public float duracion = 5f;

    void Start()
    {
        nombreItem = "PowerUp: " + tipoPowerUp.ToString();
    }

    public override void AplicarEfecto()
    {
        ItemEvents.PowerUpRecolectado(tipoPowerUp, duracion);
        Debug.Log($"PowerUp {tipoPowerUp} activado por {duracion} segundos");
    }
}
