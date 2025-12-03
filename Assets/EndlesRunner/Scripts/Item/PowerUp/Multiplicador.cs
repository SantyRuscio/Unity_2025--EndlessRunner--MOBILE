using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Multiplicador : PowerUp
{
    [Header("Configuración del PowerUp")]
    [SerializeField] private float multiplicador = 4f;
   // [SerializeField] private float duracion = 10f; //queda abierto a futuro si se quiere q cada power tenga su duracion

    private FullscreenFeatureController fullscreenFeatureController;

    private void Awake()
    {
        fullscreenFeatureController = FindObjectOfType<FullscreenFeatureController>();

        if (fullscreenFeatureController == null)
        {
            Debug.LogWarning("Multiplicador: No se encontró FullscreenFeatureController en la escena.");
        }
    }

    public override void Execute()
    {
        Debug.Log("Entre al Multiplicador");

        SoundManager.Instance.PlaySFX(AudioClip);

        EventManager.Trigger(TypeEvents.MultiplierEvent, multiplicador, GameManager.PowerUpDuration);

        if (fullscreenFeatureController != null)
        {
            fullscreenFeatureController.StartPowerUp(GameManager.PowerUpDuration);

            Debug.Log("aRRANCA SHADER");
        }
        base.TriggerEvent();

        Destroy(gameObject);
    }
}
