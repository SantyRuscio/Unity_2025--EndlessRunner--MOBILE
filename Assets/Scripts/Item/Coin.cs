using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int valor = 1;

    public AudioClip sonido;

    public override void Execute(GameObject jugador)
    {
        PuntuacionManager.Instance.AgregarMonedas(valor);

        if (sonido != null)
            AudioSource.PlayClipAtPoint(sonido, transform.position);
    }

    public override void OnDestroy()
    {

    }
}
