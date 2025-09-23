using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    public static DetectionManager instance;

    [SerializeField] private float _distanceToMove = 2f; // Valor por defecto -- este lo cambiamos con el iman cuando entra
    [SerializeField] private float _distanceToSpeed = 2f; // Valor por defecto -- este lo cambiamos con el iman cuando entra

    private float _defaultMove;
    private float _defaultSpeed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _defaultMove = _distanceToMove;
        _defaultSpeed = _distanceToSpeed;
    }

    public float CurrentDistance()
    {
        return _distanceToMove;
    }

    public float CurrentSpeed()
    {
        return _distanceToSpeed;
    }

    public void ActivateMagnet(float newDistance, float newSpeed, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(MagnetCoroutine(newDistance, newSpeed, duration));
    }

    private IEnumerator MagnetCoroutine(float newDistance, float newSpeed, float duration)
    {
        _distanceToMove = newDistance;
        _distanceToSpeed = newSpeed;

        yield return new WaitForSeconds(duration);

        _distanceToMove = _defaultMove;
        _distanceToSpeed = _defaultSpeed;
    }
}


