using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParedTouch : Item
{
    [SerializeField] float Distance = 5f;      //Proximante agregare rango de deteccion 

    private void Update()
    {
        Execute();
    }

    public override void Execute()
    {
        if (Input.touchCount > 0) // si hay al menos un dedo en pantalla
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // cuando el dedo toca la pantalla
            {
                Destroy(gameObject);
            }
        }
    }
}
