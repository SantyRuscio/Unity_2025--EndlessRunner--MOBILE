using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : Item
{
    [Header("settings de obstaculo")]
    public int daño = 1;

    void Start()
    {
        nombreItem = "obstaculo";
        valorPuntos = 0;
    }

    public override void AplicarEfecto()
    {
        ItemEvents.ObstaculoGolpeado(daño);
        Debug.Log("¡Chocaste con un obstaculo!");
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AplicarEfecto();
            // no se desactiva
        }
    }
}
