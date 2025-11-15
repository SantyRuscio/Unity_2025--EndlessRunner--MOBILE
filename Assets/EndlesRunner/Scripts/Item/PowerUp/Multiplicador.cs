using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Multiplicador : PowerUp
{
    [Header("Configuración del PowerUp")]
    [SerializeField] private float multiplicador = 4f;
    [SerializeField] private float duracion = 10f;

    [Header("Shader del PowerUp")]
    public FullscreenFeatureController shaderController;

    public override void Execute()
    {
        Debug.Log("Entre al Multiplicador");

        if (shaderController != null)
            shaderController.TurnOn();

        // Aplica efecto al juego
        EventManager.Trigger(TypeEvents.MultiplierEvent, multiplicador, duracion);

        StartCoroutine(DesactivarLuegoDeTiempo());

        base.TriggerEvent();
        Destroy(gameObject);
    }

    private IEnumerator DesactivarLuegoDeTiempo()
    {
        yield return new WaitForSeconds(duracion);

        Debug.Log("Se terminó el multiplicador, apagando shader...");

        if (shaderController != null)
            shaderController.TurnOff();
    }
}
