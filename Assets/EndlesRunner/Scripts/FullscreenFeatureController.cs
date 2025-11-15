using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullscreenFeatureController : MonoBehaviour
{
    public Material powerUpMaterial;
    public float fadeInTime = 0.5f;
    public float fadeOutTime = 1.0f;

    private const string SHADER_PROPERTY = "_Tama_oMascaraCentro";

    private const float SHADER_VALUE_OFF = 1.0f;

    private const float SHADER_VALUE_ON = 0.0f;

    private Coroutine activePowerUpCoroutine;

    void Start()
    {
        if (powerUpMaterial != null)
        {
            powerUpMaterial.SetFloat(SHADER_PROPERTY, SHADER_VALUE_OFF);
        }
    }

    public void StartPowerUp(float duration)
    {
        if (activePowerUpCoroutine != null)
        {
            StopCoroutine(activePowerUpCoroutine);
        }
        activePowerUpCoroutine = StartCoroutine(PowerUpRoutine(duration));
    }

    private IEnumerator PowerUpRoutine(float duration)
    {
        yield return StartCoroutine(AnimateShaderFloat(SHADER_VALUE_OFF, SHADER_VALUE_ON, fadeInTime));

        float waitTime = duration - fadeInTime - fadeOutTime;
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        yield return StartCoroutine(AnimateShaderFloat(SHADER_VALUE_ON, SHADER_VALUE_OFF, fadeOutTime));

        activePowerUpCoroutine = null;
    }

    private IEnumerator AnimateShaderFloat(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            powerUpMaterial.SetFloat(SHADER_PROPERTY, currentValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        powerUpMaterial.SetFloat(SHADER_PROPERTY, endValue);
    }

    private void OnDestroy()
    {
        if (powerUpMaterial != null)
        {

            powerUpMaterial.SetFloat(SHADER_PROPERTY, SHADER_VALUE_OFF);
        }
    }
}