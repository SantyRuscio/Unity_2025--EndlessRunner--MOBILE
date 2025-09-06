using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Settings del Item Base")]

    public string nombreItem = "Item";

    public int valorPuntos = 0; //si es un obstaculo da 0 puntos
    public abstract void AplicarEfecto();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AplicarEfecto();
            gameObject.SetActive(false); 
        }
    }
}
