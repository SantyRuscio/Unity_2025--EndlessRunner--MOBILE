using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Multiplicador : PowerUp
{
    [Header("Configuración del PowerUp")]
    [SerializeField] private float multiplicador = 4f; // Cuánto multiplica los metros (x2 por defecto)
    [SerializeField] private float duracion = 10f;     // Duración del efecto en segundos

    // --- ACTUALIZADO ---
    // Referencia al controlador del shader
    private FullscreenFeatureController fullscreenFeatureController;

    // --- ACTUALIZADO ---
    // Usamos Awake o Start para buscar la referencia
    private void Awake()
    {
        // Busca el objeto con el nuevo nombre de clase
        fullscreenFeatureController = FindObjectOfType<FullscreenFeatureController>();

        if (fullscreenFeatureController == null)
        {
            // Mensaje de advertencia actualizado
            Debug.LogWarning("Multiplicador: No se encontró FullscreenFeatureController en la escena.");
        }
    }

    public override void Execute()
    {
        Debug.Log("Entre al Multiplicador");

        // 1. Lógica del multiplicador (como ya lo tenías)
        EventManager.Trigger(TypeEvents.MultiplierEvent, multiplicador, duracion);

        // --- ACTUALIZADO ---
        // 2. Activar el Shader Fullscreen
        if (fullscreenFeatureController != null)
        {
            // Llama al método en la variable con el nuevo nombre
            fullscreenFeatureController.StartPowerUp(duracion);
        }
        // --- FIN ACTUALIZADO ---

        // 3. Eventos base (partículas, sonido, etc.)
        base.TriggerEvent();

        // 4. Destruir el objeto
        Destroy(gameObject);
    }
}
