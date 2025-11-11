using System.Collections;
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

            EventManager.Subscribe(TypeEcvents.ShieldEndEvent, ShieldEndEvent);
        }
    }

    private void ShieldPicked(object[] parameters)
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(true);
        }
    }

    private void ShieldEndEvent(object[] parameters)
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(TypeEcvents.ShieldEvent, ShieldPicked);

        EventManager.Unsubscribe(TypeEcvents.ShieldEndEvent, ShieldEndEvent);
    }
}


