using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
<<<<<<< HEAD
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
=======
>>>>>>> 4aa320d0c3b5e1023d6bfb42ddcca4d6f294d26c

public class ParedTouch : Item
{
    [SerializeField] float _distance = 10f;      // rango de detección
    [SerializeField] float dissolveSpeed = 0.5f; // velocidad de disolución

    private Material mat;
    private float dissolve = 0f;
    private bool isDissolving = false;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Animación de disolución
        if (isDissolving)
        {
            dissolve += Time.deltaTime * dissolveSpeed;
            mat.SetFloat("_DissolveAmount", dissolve);

            if (dissolve >= 1f)
            {
                isDissolving = false;
                Destroy(gameObject);
            }
        }
    }


    public override void Execute()
    {
        StartDissolve();
    }

    private void StartDissolve()
    {
        if (!isDissolving)
        {
            Debug.Log("Pared tocada, empezando disolución");
            isDissolving = true;
        }
    }
}