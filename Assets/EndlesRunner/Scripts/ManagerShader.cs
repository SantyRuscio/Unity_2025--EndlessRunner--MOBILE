using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public static ShaderManager instance;

    [SerializeField] private Renderer cuerpoRenderer; // objeto del player
    private Material materialInstanciado;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (cuerpoRenderer != null)
            materialInstanciado = cuerpoRenderer.material;

        EventManager.Subscribe(TypeEvents.GameOver, ResetShadders);
    }

    public void ActivarPowerMode(float duracion)
    {
        if (materialInstanciado != null)
        {
            materialInstanciado.SetFloat("_PowerMode", 1f);
            StartCoroutine(ResetShader(duracion));
        }
    }

    private IEnumerator ResetShader(float duracion)
    {
        yield return new WaitForSeconds(duracion);
        if (materialInstanciado != null)
            materialInstanciado.SetFloat("_PowerMode", 0f);
    }

    public void ResetShadders(object[] parameters)
    {
        materialInstanciado.SetFloat("_PowerMode", 0f);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEvents.GameOver, ResetShadders);
    }
}