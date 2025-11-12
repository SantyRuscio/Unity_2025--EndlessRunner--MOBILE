using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Multiplicador : PowerUp
{
    [Header("Configuración del PowerUp")]
    [SerializeField] private float multiplicador = 4f; // Cuánto multiplica los metros (x2 por defecto)
    [SerializeField] private float duracion = 10f;     //  Duración del efecto en segundos

    public override void Execute()
    {
        Debug.Log("Entre al Multiplicador");

        EventManager.Trigger(TypeEvents.MultiplierEvent, multiplicador, duracion);

        base.TriggerEvent();

        // Destruye el objeto del PowerUp
        Destroy(gameObject);
    }

}
