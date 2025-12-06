using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDebuf : Item
{
    [Header("Configuración Debuf")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float duracion = 8f;


    private Renderer miRenderer;
    private Collider miCollider;

    private void Start()
    {
        miRenderer = GetComponent<Renderer>();
        miCollider = GetComponent<Collider>();
    }

    public override void Execute()
    {
        Debug.Log("ACTIVADO: PIXEL DEBUF");

        if (audioClip != null)
            SoundManager.Instance.PlaySFX(audioClip);

        if (PixelEffectManagerr.Instance != null)
        {
            PixelEffectManagerr.Instance.ActivarGlitch(duracion);
        }
        else
        {
            Debug.LogWarning("¡Falta el PixelEffectManager en la escena!");
        }

        ResetObject();
    }

    public override void ResetObject()
    {

        if (miRenderer) miRenderer.enabled = true;
        if (miCollider) miCollider.enabled = true;

        base.ResetObject();
    }
}
