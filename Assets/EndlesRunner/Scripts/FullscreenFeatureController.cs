using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FullscreenFeatureController : MonoBehaviour
{
    [SerializeField] private UniversalRendererData rendererData;
    [SerializeField] private string featureName;

    private ScriptableRendererFeature feature;

    void Awake()
    {
        foreach (var f in rendererData.rendererFeatures)
        {
            if (f.name == featureName)
            {
                feature = f;
                break;
            }
        }

        if (feature == null)
            Debug.LogError("Renderer Feature NO encontrada: " + featureName);
    }

    public void TurnOn()
    {
        if (feature == null) return;

        feature.SetActive(true);

        rendererData.SetDirty();
    }

    public void TurnOff()
    {
        if (feature == null) return;

        feature.SetActive(false);

        rendererData.SetDirty();
    }
}