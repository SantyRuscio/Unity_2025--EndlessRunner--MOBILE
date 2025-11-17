using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectController : MonoBehaviour
{
    public Material deathMaterial;

    private const string SHADER_PROPERTY = "_Oscurecimiento_Vi_eta";

    void Start()
    {
        HideDeathScreen();
    }

    public void ShowDeathScreen()
    {
        if (deathMaterial != null)
        {
            deathMaterial.SetFloat(SHADER_PROPERTY, 1f);
        }
    }
    public void HideDeathScreen()
    {
        if (deathMaterial != null)
        {
            deathMaterial.SetFloat(SHADER_PROPERTY, 0f);
        }
    }
}
