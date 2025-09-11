using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
        Execute();

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
        float currentDistance = Vector3.Distance(transform.position, GameManager.instance.GetPlayerModel().transform.position);

        if (currentDistance <= _distance)
        {
            // PC
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                StartDissolve();
            }
#endif

            // Móvil
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartDissolve();
            }
#endif
        }
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