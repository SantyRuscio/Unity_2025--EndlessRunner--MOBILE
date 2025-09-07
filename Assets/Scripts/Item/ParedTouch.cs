using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ParedTouch : Item
{
    [SerializeField] private float _distance = 7f; // rango de deteccion

    public override void Execute(GameObject jugador)
    {
        if (jugador == null) return;

        // calcula distancia entre jugador y pared
        float currentDistance = Vector3.Distance(jugador.transform.position, transform.position);
        Debug.Log("Distancia actual es " + currentDistance);

        if (currentDistance <= _distance)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            // PC - click del mouse
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pared tocada con click");
                Destroy(gameObject);
            }
#endif

#if UNITY_ANDROID || UNITY_IOS
            // Móvil - touch
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Pared tocada con touch");
                    Destroy(gameObject);
                }
            }
#endif
        }
    }
}