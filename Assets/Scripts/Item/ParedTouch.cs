using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ParedTouch : Item
{
    [SerializeField] float _distance = 7f;      // Proximamente agregare rango de deteccion 

    public override void Execute()
    {
        Debug.Log("Entre a Execute()");

        if (Input.touchCount > 0) // si hay al menos un dedo en pantalla
        {
            Touch touch = Input.GetTouch(0);

            var player = GameManager.instance.GetPlayerModel();

            float currentDistance = Vector3.Distance(player.transform.position, transform.position);

            Debug.Log("Distancia actual es " + currentDistance);

            if (touch.phase == TouchPhase.Began && currentDistance <= _distance)
            {
                Debug.Log("entre al ultima etapa pared!");
                Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        Execute();   //Hay que ver como sacar este upodate de aca
    }

    private void OnMouseEnter()
    {
        Debug.Log("toque click");
        Execute();
    }
}
