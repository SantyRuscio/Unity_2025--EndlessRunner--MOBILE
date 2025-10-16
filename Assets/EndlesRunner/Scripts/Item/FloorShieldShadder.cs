using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class FloorShieldShader : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private bool disableOnAwake = true; 

    private void Awake()
    {
        if (disableOnAwake && shieldObject != null)
        {
            shieldObject.SetActive(false);

            Debug.Log("SUSCRITO al SHIELDS");
            EventManager.Subscribe(TypeEcvents.ShieldEvent, ShieldPicked);
        }
    }

    private void ShieldPicked(object[] parameters)
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEcvents.ShieldEvent, ShieldPicked);
    }
}
