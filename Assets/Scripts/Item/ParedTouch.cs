using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ParedTouch : Item
{
    [SerializeField] float _distance = 10f;      // rango de detección 

    private void Update()
    {
        Execute();
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
                Debug.Log("Pared tocada con click");
                Destroy(gameObject);
            }
#endif

            // Móvil
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Pared tocada con touch");
                Destroy(gameObject);
            }
#endif
        }
    }
}